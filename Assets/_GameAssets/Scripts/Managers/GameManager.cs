using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance{get; private set;}
    [Header("Referances")]
    [SerializeField] private EggCounterUI _eggCounterUI;

    [Header("Settings")]
    [SerializeField] private int _maxEggCount = 5;
    private int _currentEggCount = 0;

    void Awake()
    {
        Instance = this;
    }
    public void OnEggCollected(){
        _currentEggCount++;
        _eggCounterUI.SetEggCounterText(_currentEggCount,_maxEggCount);
        if(_currentEggCount==_maxEggCount){
            _eggCounterUI.SetEggCompleted();
        }
    }

}
