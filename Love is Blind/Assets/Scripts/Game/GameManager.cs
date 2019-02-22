using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public static bool GameIsPaused => Instance.GameState == GameState.Pause || Instance.GameState == GameState.GameOver;

    public bool autoStartPlay = true;
    public GameOverMenu gameOverMenu;

    private GameState _gameState;
    private GameResult _gameResult;

    public GameState GameState => _gameState;
    public GameResult GameResult => _gameResult;

    protected override void Awake()
    {
        base.Awake();

        _gameState = GameState.Ready;
    }

    private void Start()
    {
        if (autoStartPlay)
            Play();
    }

    public void Play()
    {
        _gameState = GameState.Playing;

        Time.timeScale = 1f;

        gameOverMenu.HideUI();
    }

    public void Pause()
    {
        _gameState = GameState.Pause;

        Time.timeScale = 0f;
    }

    public void GameOver(GameResult result)
    {
        _gameResult = result;
        _gameState = GameState.GameOver;

        gameOverMenu.ShowUI(result);
    }
}

public enum GameState
{
    Loading,
    Ready,
    Playing,
    Pause,
    GameOver
}

public enum GameResult
{
    Win,
    Lose
}