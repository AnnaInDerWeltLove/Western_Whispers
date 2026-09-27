using UnityEngine;
using UnityEngine.UI;

// KI generiert
public class UISoundSetting : MonoBehaviour
{
    [Header("Lautstärke")]
    [SerializeField] private Slider volumeSlider;

    [Header("Sound")]
    [SerializeField] private Toggle soundToggle;
    
    [Header("Vollbild")]
    [SerializeField] private Toggle fullscreenToggle;

    private void Start()
    {
        if (SoundManager.Instance == null)
        {
            Debug.LogWarning("Kein SoundManager gefunden.", this);
            return;
        }

        volumeSlider.SetValueWithoutNotify(
            SoundManager.Instance.MasterVolume
        );

        soundToggle.SetIsOnWithoutNotify(
            !SoundManager.Instance.IsMuted
        );
        bool isFullscreen = PlayerPrefs.GetInt(
            "Fullscreen",
            Screen.fullScreen ? 1 : 0
        ) == 1;

        fullscreenToggle.SetIsOnWithoutNotify(isFullscreen);

        volumeSlider.onValueChanged.AddListener(SetVolume);
        soundToggle.onValueChanged.AddListener(SetSoundEnabled);
    }

    private void SetVolume(float volume)
    {
        SoundManager.Instance.SetMasterVolume(volume);
    }

    private void SetSoundEnabled(bool soundEnabled)
    {
        SoundManager.Instance.SetMuted(!soundEnabled);
    }

    private void OnDestroy()
    {
        volumeSlider.onValueChanged.RemoveListener(SetVolume);
        soundToggle.onValueChanged.RemoveListener(SetSoundEnabled);
    }
    public void SetFullscreen(bool fullscreen)
    {
        Screen.fullScreen = fullscreen;

        PlayerPrefs.SetInt("Fullscreen", fullscreen ? 1 : 0);
        PlayerPrefs.Save();
    }
}