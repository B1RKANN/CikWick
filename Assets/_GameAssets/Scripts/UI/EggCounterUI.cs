using DG.Tweening;
using TMPro;
using UnityEngine;

public class EggCounterUI : MonoBehaviour
{
    [Header("Referances")]
    [SerializeField] private TMP_Text _eggCounterText;

    private RectTransform _eggCounterRectTransform;

    [Header("Settings")]
    [SerializeField] private Color _eggCounterColor;
    [SerializeField] private float _colorDuration;
    [SerializeField] private float _scaleDuration;

    void Awake()
    {
        _eggCounterRectTransform = _eggCounterText.gameObject.GetComponent<RectTransform>();
    }
    public void SetEggCounterText(int counter,int max){
        _eggCounterText.text = counter.ToString()+"/"+max.ToString();
    }

    public void SetEggCompleted(){
        _eggCounterText.DOColor(_eggCounterColor,_colorDuration);
        _eggCounterRectTransform.DOScale(1.2f,_scaleDuration).SetEase(Ease.OutBack);
        
    }
}
