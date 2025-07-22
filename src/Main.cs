using Godot;
using System;
using FlappyClone.Core;

public partial class Main : Node
{
	public Vector2 screenSize;
	private ScoreHandler scoreHandler;
	private Pipe[] pipes = new Pipe[25];

	public override void _Ready()
	{
		scoreHandler = new ScoreHandler();
		screenSize = GetViewport().GetVisibleRect().Size;
	}
	
	public void NewGame() {
		scoreHandler.Reset();
		initHud();
		initBird();
		initPipes();
		GetNode<Timer>("ScoreTimer").Start();
	}
	
	private void initPipes() {
		Random rand = new Random();
		PackedScene pipeScene = GD.Load<PackedScene>("res://src/scenes/pipe.tscn");
		int xPosTop = 800;
		int xPosBottom = 800;
		int minOffset = 80;
		int maxOffset = 200;
		for (int i = 0; i < 25; i++) {
			Pipe pipe = (Pipe)pipeScene.Instantiate();
			int height = rand.Next(200, 320);
			if (rand.NextDouble() <= 0.5) {
				xPosBottom += rand.Next(minOffset, maxOffset);
				pipe.Initialize(false, height, xPosBottom);
			} else {
				xPosTop += rand.Next(minOffset, maxOffset);
				pipe.Initialize(true, height, xPosTop);
			}
			AddChild(pipe);
			pipes[i] = pipe;
		}
	}
	
	private void initHud() {
		var Hud = GetNode<Hud>("Hud");
		Hud.SetScore(0);
		Hud.HideMessage();
	}
	
	private void initBird() {
		var bird = GetNode<Bird>("Bird");
		var startPosition = new Vector2(350, 350);
		bird.Start(startPosition);
	}

	public void GameOver()
	{
		Array.ForEach(pipes, pipe => pipe.QueueFree());
		Array.Clear(pipes);
		GetNode<Bird>("Bird").gameRunning = false;
		GetNode<Hud>("Hud").ShowGameOver();
		GetNode<Timer>("ScoreTimer").Stop();
	}

	public void OnScoreTimerTimeout() {
		GetNode<Hud>("Hud").SetScore(scoreHandler.IncrementScore());
	}
}
