using Godot;
using System;

public partial class GameManager : Node
{
    private const string HIGHSCORE_PATH = "user://hs.dat";

    public int Score { get; set; }
    public int Highscore { get; set; }
    public bool GameOver { get; set; }

    [Export]
    public Control UI { get; set; }
    [Export]
    public Node Audio { get; set; }
    [Export]
    public Control GameOverScreen { get; set; }

    private Label _scoreLabel;
    private Label _highscoreLabel;
    private AudioStreamPlayer _gameMusic;
    private AudioStreamPlayer _checkpointSfx;
    private AudioStreamPlayer _gameOverMusic;

    public override void _Ready()
    {
        _scoreLabel = UI.GetNode<Label>("Score");
        _highscoreLabel = UI.GetNode<Label>("Highscore");
        _gameMusic = Audio.GetNode<AudioStreamPlayer>("GameMusic");
        _checkpointSfx = Audio.GetNode<AudioStreamPlayer>("CheckpointSFX");
        _gameOverMusic = Audio.GetNode<AudioStreamPlayer>("GameOverMusic");
        SetHighscore(LoadHighscore());
        var restartBtn = GameOverScreen.GetNode<Button>("Restart");
        restartBtn.Pressed += RestartGame;
        var mainMenuBtn = GameOverScreen.GetNode<Button>("MainMenu");
        mainMenuBtn.Pressed += GoToMainMenu;
    }

    public void AddScore(int scoreToAdd = 1)
    {
        if (GameOver) return;

        Score += scoreToAdd;
        _scoreLabel.Text = $"{Score}";
        _checkpointSfx.Play();
    }

    public void SetGameOver()
    {
        if (GameOver) return;

        GameOver = true;
        GameOverScreen.Visible = true;
        _gameMusic.Stop();
        _gameOverMusic.Play();

        if (Score > Highscore)
        {
            SetHighscore(Score);
            SaveHighscore(Score);
        }
    }

    private void SetHighscore(int score)
    {
        Highscore = score;
        _highscoreLabel.Text = $"Highscore: {Highscore}";
    }

    public void RestartGame()
    {
        GetTree().ReloadCurrentScene();
    }

    public void GoToMainMenu()
    {
        GetTree().ChangeSceneToFile("res://scenes/main_menu.tscn");
    }

    private int LoadHighscore()
    {
        if (!FileAccess.FileExists(HIGHSCORE_PATH)) return 0;

        using var file = FileAccess.Open(HIGHSCORE_PATH, FileAccess.ModeFlags.Read);
        var encoded = file.GetAsText();

        return BitConverter.ToInt32(Marshalls.Base64ToRaw(encoded));
    }

    private void SaveHighscore(int score)
    {
        var encoded = Marshalls.RawToBase64(BitConverter.GetBytes(score));
        using var file = FileAccess.Open("user://hs.dat", FileAccess.ModeFlags.Write);
        file.StoreString(encoded);
    }
}
