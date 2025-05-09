using Godot;
using System;

public partial class PipeSpawner : Node2D
{
	[Export]
	public int HeightOffset { get; set; } = 190;
	[Export]
	public float Speed { get; set; } = -400.0f;
	[Export]
	public float KillZone { get; set; } = -750.0f;
	[Export]
	public GameManager GameManager { get; set; }

	private PackedScene _pipeScene;
	private Timer _timer;

	public override void _Ready()
	{
		_pipeScene = GD.Load<PackedScene>("res://scenes/pipes.tscn");
		_timer = GetNode<Timer>("Timer");
		_timer.Timeout += OnTimeout;

		SpawnPipe();
		_timer.Start();
	}

	private void OnTimeout()
	{
		if (GameManager.GameOver) return;

		SpawnPipe();
		_timer.Start();
	}

	private void SpawnPipe()
	{
		var pipe = _pipeScene.Instantiate<Pipes>();
		pipe.Speed = Speed;
		pipe.KillZone = KillZone;
		pipe.Position = pipe.Position with { Y = Random.Shared.Next(-HeightOffset, HeightOffset + 1) };
		pipe.GameManager = GameManager;
		AddChild(pipe);
	}
}
