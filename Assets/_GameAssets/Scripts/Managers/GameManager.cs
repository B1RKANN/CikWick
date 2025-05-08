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
    private bool _isCatCathed;

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
        if(!_isCatCathed){
             _playerHealthUI.AnimateDamageForAll();
            StartCoroutine(OnGameOver(true));
            CameraShake.Instance.ShakeCamera(1.5f,2f,0.5f);
            _isCatCathed = true;
        }
       
    }

    private void HealthManager_OnPlayerDeath()
    {
        StartCoroutine(OnGameOver(false));
    }

    void OnEnable()
    {
        ChangeGameState(GameState.CutScene);
        BackgroundMusic.Instance.PlayBackgroundMusic(true);
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
    private IEnumerator OnGameOver(bool isCatcathed){
        yield return new WaitForSeconds(_delay);
        ChangeGameState(GameState.GameOver);
        _winLoseUI.OnGameLose();
        if(isCatcathed){AudioManager.Instance.Play(SoundType.CatSound);}
    }
    public GameState GetCurrentGameState(){
        return _currentGameState;
    }
}
