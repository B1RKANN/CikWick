using System;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance{get; private set;}

    public event Action<GameState> onGameStateChanged;
    [Header("Referances")]
    [SerializeField] private CatController _catController;
    [SerializeField] private EggCounterUI _eggCounterUI;
    [SerializeField] private WinLoseUI _winLoseUI;
    [SerializeField] private PlayerHealthUI _playerHealthUI;

    [Header("Settings")]
    [SerializeField] private int _maxEggCount = 5;
    [SerializeField] private float _delay;
    private int _currentEggCount = 0;

    private GameState _currentGameState;

    void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        HealthManager.Instance.OnPlayerDeath += HealthManager_OnPlayerDeath;
        _catController.OnCatCatched += CatController_OnCatched;
    }

    private void CatController_OnCatched()
    {   
        _playerHealthUI.AnimateDamageForAll();
        StartCoroutine(OnGameOver());
    }

    private void HealthManager_OnPlayerDeath()
    {
        StartCoroutine(OnGameOver());
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
            _winLoseUI.OnGameWin();
        }
    }
    private IEnumerator OnGameOver(){
        yield return new WaitForSeconds(_delay);
        ChangeGameState(GameState.GameOver);
        _winLoseUI.OnGameLose();
    }
    public GameState GetCurrentGameState(){
        return _currentGameState;
    }
}
