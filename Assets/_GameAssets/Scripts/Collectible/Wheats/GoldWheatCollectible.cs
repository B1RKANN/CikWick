using UnityEngine;
using UnityEngine.UI;

public class GoldWheatCollectible : MonoBehaviour,ICollectible
{
  [SerializeField] private WheatDesignSO _wheatDesignSO;
  [SerializeField] private PlayerController _playerController;
  [SerializeField] private PlayerStateUI _playerStateUI;

  private RectTransform _playerBoosterTransform;
  private Image _playerBoosterImage;

    void Awake()
    {
        _playerBoosterTransform = _playerStateUI.GetBoosterSpeedTransform;
        _playerBoosterImage = _playerBoosterTransform.GetComponent<Image>();

    }

    public void Collect(){
    _playerController.SetMovementSpeed(_wheatDesignSO.IncreaseDecraeseMultiplier,_wheatDesignSO.ResetBoostDuration);
    _playerStateUI.PlayeBoosterUIAnimations(_playerBoosterTransform,_playerBoosterImage,_playerStateUI.GetGoldBoosterWheatImage,
    _wheatDesignSO.ActiveSprite,_wheatDesignSO.PassiveSprite,_wheatDesignSO.ActiveWheatSprite,
    _wheatDesignSO.PassiveWheatSprite,_wheatDesignSO.ResetBoostDuration);
    Destroy(gameObject);
    AudioManager.Instance.Play(SoundType.PickupGoodSound);
  }
}
