using Godot;
using System;

public partial class Pipe : Area2D
{
	public bool Top { get; private set; }
	public int Height { get; private set; }
	public int YPos {get; private set; }

	[Export]
	public int Speed {get; set; } = 75;

	private int _OriginalHeight = 30;
	
	public void Initialize(bool top, int height, int xPos)
	{
		YPos = top ? 0 : (750 - height) + 50;
		double heightRatio = height / _OriginalHeight;
		Height = (int)Math.Round(heightRatio);
		Top = top;
		Scale = new Vector2(1, Height);
		Vector2 initPos = new Vector2(xPos, YPos);
		Position = initPos;
		Show();
	}
	
	public override void _Ready() {
	}

	public override void _Process(double delta)
	{
		float newXPos = Position.X - (float) (Speed * delta);
		Vector2 position = new Vector2(newXPos, YPos);
		Position = position;
	}
}
