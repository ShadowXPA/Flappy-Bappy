using Godot;
using System;

public partial class Pointzone : Area2D
{
    [Export]
    public GameManager GameManager { get; set; }

    public override void _Ready()
    {
        BodyEntered += OnBodyEntered;
    }

    private void OnBodyEntered(Node2D body)
    {
        SetDeferred(Area2D.PropertyName.Monitoring, false);
        GameManager.AddScore();
    }
}
