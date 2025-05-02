using UnityEngine;

public class HolyWheatCollectible : MonoBehaviour,ICollectible
{
  [SerializeField] private WheatDesignSO _wheatDesignSO;
  [SerializeField] private PlayerController _playerController;

  public void Collect(){
    _playerController.SetJumpForce(_wheatDesignSO.IncreaseDecraeseMultiplier,_wheatDesignSO.ResetBoostDuration);
    Destroy(gameObject);
  }
}
