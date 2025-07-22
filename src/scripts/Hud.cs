using Godot;
using System;

public partial class Hud : CanvasLayer
{
	[Signal]
	public delegate void StartGameEventHandler();
	
	public void ShowMessage(string text)
	{
		var message = GetNode<Label>("Message");
		message.Text = text;
		message.Show();
	}
	
	public void HideMessage() {
		GetNode<Label>("Message").Hide();
	}
	
	public void ShowGameOver() {
		ShowMessage("Game Over");

		//var messageTimer = GetNode<Timer>("MessageTimer");
		//await ToSignal(messageTimer, Timer.SignalName.Timeout);

		var message = GetNode<Label>("Message");
		message.Text = "Game over!";
		message.Show();

		//await ToSignal(GetTree().CreateTimer(1.0), SceneTreeTimer.SignalName.Timeout);
		GetNode<Button>("StartButton").Show();
	}
	
	private void OnStartButtonPressed()
	{
		GetNode<Button>("StartButton").Hide();
		EmitSignal(SignalName.StartGame);
		
	}
	
	public void SetScore(int score)
	{
		GD.Print("Score: " + score);
		GetNode<Label>("ScoreLabel").Text = score.ToString();
	}
}
