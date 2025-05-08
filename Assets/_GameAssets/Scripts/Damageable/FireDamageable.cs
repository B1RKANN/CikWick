using UnityEngine;

public class FireDamageable : MonoBehaviour, IDamageable
{   
    [SerializeField] private float _force = 10f;


    public void GiveDamage(Rigidbody playerRigidBody, Transform playerVisualTransform)
    {
        HealthManager.Instance.Damage(1);
        playerRigidBody.AddForce(-playerVisualTransform.forward*_force,ForceMode.Impulse);
        AudioManager.Instance.Play(SoundType.ChickSound);
        Destroy(gameObject);
    }
}
