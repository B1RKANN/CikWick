using UnityEngine;

public class PlayerInteractionController : MonoBehaviour
{   
    [SerializeField] private Transform _playerVisualTransform;
    private PlayerController _playerController;
    private Rigidbody _playerRigidBody;


    void Awake()
    {
        _playerController = GetComponent<PlayerController>();
        _playerRigidBody = GetComponent<Rigidbody>();
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
    void OnParticleCollision(GameObject other)
    {
        if(other.TryGetComponent<IDamageable>(out var damageable)){
            damageable.GiveDamage(_playerRigidBody,_playerVisualTransform);
        }
    }
}
