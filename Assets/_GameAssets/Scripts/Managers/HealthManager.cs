using UnityEngine;

public class HealthManager : MonoBehaviour
{
    [SerializeField] private int _maxHealth = 3;
    private int _currentHealth;

    void Start()
    {
        _currentHealth = _maxHealth;
    }

    public void Damage(int damageAmount){
        if(_currentHealth>0){
             _currentHealth -= damageAmount ;
             //UI ANİMATE
             if(_currentHealth <=0)
             {
                //player dead
             }
        }
       
    }

}
