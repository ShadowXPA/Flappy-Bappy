using Godot;
using System;

public partial class MainMenuManager : Node
{   
    [Export]
    public Control UI { get; set; }

    public override void _Ready()
    {
        var playButton = UI.GetNode<Button>("Play");
        var quitButton = UI.GetNode<Button>("Quit");
        playButton.Pressed += StartGame;
        quitButton.Pressed += QuitGame;
    }

    public void StartGame()
    {
        GetTree().ChangeSceneToFile("res://scenes/game.tscn");
    }

    public void QuitGame()
    {
        GetTree().Quit();
    }
}
