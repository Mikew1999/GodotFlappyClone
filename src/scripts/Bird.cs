using Godot;
using System;

public partial class Bird : CharacterBody2D
{
	[Signal]
	public delegate void HitEventHandler();
	
	[Export]
	public float jumpSpeed { get; set; } = 200f;
	public float minYSpeed { get; set; } = -350f;
	public float maxYSpeed { get; set; } = 500f;
	public bool gameRunning { get; set; } = false;

	public Vector2 ScreenSize;
	
	private const float Gravity = 400.0f;
	
	public override void _Input(InputEvent @event)
	{
		if (@event.IsActionPressed("jump"))
		{
			var velocity = Velocity;
			velocity.Y -= jumpSpeed;
			velocity.Y = Mathf.Clamp(velocity.Y, minYSpeed, maxYSpeed);
			Velocity = velocity;
		}
	}
	
	public override void _PhysicsProcess(double delta)
	{
		if (!gameRunning)
			return;
		var velocity = Velocity;
		velocity.Y += (float)delta * Gravity;
		Velocity = velocity;
		MoveAndSlide();
		if (_HitWall()) {
			_HandleHit();
		}
	}
	
	private bool _HitWall() {
		return (
			Position.Y < 0
			|| Position.Y > ScreenSize.Y
		);
	}
	
	public override void _Ready()
	{
		ScreenSize = GetViewportRect().Size;
		Hide();
	}

	public override void _Process(double delta)
	{
	}
	
	private void OnBodyEntered(Node2D body)
	{
		_HandleHit();
	}
	
	private void _HandleHit() {
		Hide();
		EmitSignal(SignalName.Hit);
		GetNode<CollisionShape2D>("CollisionShape2D").SetDeferred(CollisionShape2D.PropertyName.Disabled, true);
	}
	
	public void Start(Vector2 position)
	{
		var velocity = Vector2.Zero;
		Velocity = velocity;
		Position = position;
		Show();
		GetNode<CollisionShape2D>("CollisionShape2D").Disabled = false;
		var animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		animatedSprite2D.Play();
		gameRunning = true;
	}
	
}
