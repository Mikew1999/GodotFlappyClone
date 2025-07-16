using Godot;
using System;

public partial class Main : Node
{
	private int _score;

	public override void _Ready()
	{
	}
	
	public void NewGame() {
		GD.Print("game started");
		var bird = GetNode<Bird>("Bird");
		var startPosition = new Vector2(350, 350);
		var Hud = GetNode<Hud>("Hud");
		Hud.HideMessage();
		bird.Start(startPosition);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	
	public void GameOver()
	{
		GetNode<Bird>("Bird").gameRunning = false;
		GetNode<Hud>("Hud").ShowGameOver();
	}
}
