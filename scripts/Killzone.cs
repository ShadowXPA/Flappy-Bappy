using Godot;
using System;

public partial class Killzone : Area2D
{
    [Export]
    public GameManager GameManager { get; set; }

    public override void _Ready()
    {
        BodyEntered += OnBodyEntered;
    }

    private void OnBodyEntered(Node2D body)
    {
        GameManager.SetGameOver();
    }
}
