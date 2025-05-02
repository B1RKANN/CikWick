using UnityEngine;

public class PlayerInterationController : MonoBehaviour
{   
    private PlayerController _playerController;

    void Awake()
    {
        _playerController = GetComponent<PlayerController>();
    }
    private void OnTriggerEnter(Collider other)
    {
       if(other.gameObject.TryGetComponent<ICollectible>(out var collectible)){
        collectible.Collect();
       }
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.TryGetComponent<IBoostables>(out var boostable)){
            boostable.Boost(_playerController);
        }
    }
}
