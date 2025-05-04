using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance{get; private set;}

    public event Action<GameState> onGameStateChanged;
    [Header("Referances")]
    [SerializeField] private EggCounterUI _eggCounterUI;

    [Header("Settings")]
    [SerializeField] private int _maxEggCount = 5;
    private int _currentEggCount = 0;

    private GameState _currentGameState;

    void Awake()
    {
        Instance = this;
    }

    void OnEnable()
    {
        ChangeGameState(GameState.Play);
    }
    public void ChangeGameState(GameState gameState){
        onGameStateChanged?.Invoke(gameState);
        _currentGameState = gameState;
        Debug.Log("Game State : "+ gameState);
    }
    public void OnEggCollected(){
        _currentEggCount++;
        _eggCounterUI.SetEggCounterText(_currentEggCount,_maxEggCount);
        if(_currentEggCount==_maxEggCount){
            _eggCounterUI.SetEggCompleted();
            ChangeGameState(GameState.GameOver);
        }
    }
    public GameState GetCurrentGameState(){
        return _currentGameState;
    }
}
