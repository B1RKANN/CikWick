using System;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public static HealthManager Instance{get; private set;}
    public event Action OnPlayerDeath;
    [Header("References")]
    [SerializeField] private PlayerHealthUI _playerHealtUI;
    [Header("Settings")]
    [SerializeField] private int _maxHealth = 3;
    private int _currentHealth;

    void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        _currentHealth = _maxHealth;
    }

    public void Damage(int damageAmount){
        if(_currentHealth>0){
             _currentHealth -= damageAmount ;
             _playerHealtUI.AnimateDamage();
             if(_currentHealth <=0)
             {
                OnPlayerDeath?.Invoke();
             }
        }
       
    }

}
