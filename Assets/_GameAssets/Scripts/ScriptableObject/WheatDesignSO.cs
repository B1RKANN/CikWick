using UnityEngine;
[CreateAssetMenu(fileName = "WheatDesignSO",menuName ="ScriptableObjects/WheatDesignSO")]
public class WheatDesignSO : ScriptableObject
{
    [SerializeField] private float _increaseDecraeseMultiplier;
    [SerializeField] private float _resetBoostDuration;

    public float IncreaseDecraeseMultiplier => _increaseDecraeseMultiplier;
    public float ResetBoostDuration => _resetBoostDuration;
}
