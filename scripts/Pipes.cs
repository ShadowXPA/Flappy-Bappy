using Godot;
using System;

public partial class Pipes : Node2D
{
	[Export]
	public float Speed { get; set; } = -400.0f;
	[Export]
	public float KillZone { get; set; } = -750.0f;
	[Export]
	public GameManager GameManager { get; set; }

	private Killzone _killzone;
	private Pointzone _pointzone;

	public override void _Ready()
	{
		_killzone = GetNode<Killzone>("Killzone");
		_pointzone = GetNode<Pointzone>("Pointzone");
		_killzone.GameManager = GameManager;
		_pointzone.GameManager = GameManager;
	}

	public override void _Process(double delta)
	{
		if (GameManager.GameOver) return;

		var position = Position;
		position.X += Speed * (float)delta;
		Position = position;

		if (Position.X < KillZone)
		{
			QueueFree();
		}
	}
}
