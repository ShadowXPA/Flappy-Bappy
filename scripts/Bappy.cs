using Godot;
using System;

public partial class Bappy : CharacterBody2D
{
	[Export]
	public float Speed = 300.0f;
	[Export]
	public float JumpVelocity = -400.0f;
    [Export]
    public GameManager GameManager { get; set; }

	private AnimationPlayer _animationPlayer;

    public override void _Ready()
    {
		_animationPlayer = GetNode<AnimationPlayer>("BappyAnimation");
    }

	public override void _PhysicsProcess(double delta)
	{
		if (GameManager.GameOver) return;

		Vector2 velocity = Velocity;

		if (!IsOnFloor())
		{
			velocity += GetGravity() * (float)delta;
		}

		var animation = _animationPlayer.CurrentAnimation;

		if (Input.IsActionJustPressed("flap") && animation == "")
		{
			_animationPlayer.Play("flap");
			velocity.Y = JumpVelocity;
		}

		Velocity = velocity;
		MoveAndSlide();
	}
}
