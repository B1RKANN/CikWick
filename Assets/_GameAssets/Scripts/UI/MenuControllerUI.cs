using MaskTransitions;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuControllerUI : MonoBehaviour
{
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _quitButton;
    void Awake()
    {
        _playButton.onClick.AddListener(()=>{
            AudioManager.Instance.Play(SoundType.TransitionSound);
            TransitionManager.Instance.LoadLevel(Consts.SceneNames.GAME_SCENE);
        });
        _quitButton.onClick.AddListener(()=>{
            AudioManager.Instance.Play(SoundType.ButtonClickSound);
            Application.Quit();
        });
    }
}
