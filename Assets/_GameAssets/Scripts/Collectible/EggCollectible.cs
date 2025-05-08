using UnityEngine;

public class EggCollectible : MonoBehaviour, ICollectible
{
    private bool _isCollected = false;
    
    public void Collect()
    {
        if (_isCollected) return;
        
        _isCollected = true;
        if (GetComponent<Collider>()) 
        {
            GetComponent<Collider>().enabled = false;
        }
        GameManager.Instance.OnEggCollected();
         AudioManager.Instance.Play(SoundType.PickupGoodSound);
        Destroy(gameObject);
    }
}
