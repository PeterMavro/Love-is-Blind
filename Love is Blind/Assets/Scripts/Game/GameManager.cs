using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public bool autoStartPlay = true;

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
    }

    public void Pause()
    {
        _gameState = GameState.Pause;
    }

    public void GameOver()
    {
        _gameState = GameState.GameOver;

        CheckResult();
    }

    private void CheckResult()
    {

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