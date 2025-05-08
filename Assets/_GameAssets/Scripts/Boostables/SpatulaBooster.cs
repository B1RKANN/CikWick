using UnityEngine;

public class SpatulaBooster : MonoBehaviour, IBoostables
{
    [Header("Referances")]
    [SerializeField] private Animator _spatulaAnimator;
    [Header("Settings")]
    [SerializeField] private float _jumpForce;
    private bool _isActivated;
    public void Boost(PlayerController playerController)
    {
        if(_isActivated){return;}
        PlayerBoostAnimation();
        Rigidbody playerRigidBody = playerController.GetPlayerRigidBody();

        playerRigidBody.linearVelocity = new Vector3(playerRigidBody.linearVelocity.x,0f,playerRigidBody.linearVelocity.z);
        playerRigidBody.AddForce(transform.forward*_jumpForce,ForceMode.Impulse);
        _isActivated = true;
        Invoke(nameof(ResetActivation),0.2f);
        AudioManager.Instance.Play(SoundType.SpatulaSound);
    }

    private void PlayerBoostAnimation(){
        _spatulaAnimator.SetTrigger(Consts.OtherAnimations.IS_SPATULAJUMPİNG);
    }

    private void ResetActivation(){
        _isActivated = false;
    }
}
