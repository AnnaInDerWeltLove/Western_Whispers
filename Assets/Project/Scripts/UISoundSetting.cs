using UnityEngine;
using UnityEngine.UI;

public class UISoundSetting : MonoBehaviour
{
    [Header("Lautstärkeregler")]
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider soundEffectSlider;
    [SerializeField] private Slider playerLaserSlider;
    [SerializeField] private Slider enemyLaserSlider;
    [SerializeField] private Slider bossLaserSlider;

    [Header("Stummschaltung")]
    [SerializeField] private Toggle muteToggle;

    private void Start()
    {
        if (SoundManager.Instance == null)
        {
            Debug.LogWarning("Kein SoundManager gefunden.", this);
            return;
        }

        SetInitialValues();
        RegisterListeners();
    }

    private void SetInitialValues()
    {
        musicSlider.value = SoundManager.Instance.MusicVolume;

        soundEffectSlider.value = SoundManager.Instance.SoundEffectVolume;

        playerLaserSlider.value = SoundManager.Instance.PlayerLaserVolume;
       
        enemyLaserSlider.value = SoundManager.Instance.EnemyLaserVolume;

        bossLaserSlider.value = SoundManager.Instance.BossLaserVolume;

        muteToggle.isOn = SoundManager.Instance.IsMuted;
    }

    private void RegisterListeners()
    {
        musicSlider.onValueChanged.AddListener(SoundManager.Instance.SetMusicVolume);
        soundEffectSlider.onValueChanged.AddListener(SoundManager.Instance.SetSoundEffectVolume);
        playerLaserSlider.onValueChanged.AddListener(SoundManager.Instance.SetPlayerLaserVolume);
        enemyLaserSlider.onValueChanged.AddListener(SoundManager.Instance.SetEnemyLaserVolume);
        bossLaserSlider.onValueChanged.AddListener(SoundManager.Instance.SetBossLaserVolume);
        muteToggle.onValueChanged.AddListener(SoundManager.Instance.SetMuted);
    }

    private void OnDestroy()
    {
        if (SoundManager.Instance == null)
        {
            return;
        }

        musicSlider.onValueChanged.RemoveListener(SoundManager.Instance.SetMusicVolume);
        soundEffectSlider.onValueChanged.RemoveListener(SoundManager.Instance.SetSoundEffectVolume);
        playerLaserSlider.onValueChanged.RemoveListener(SoundManager.Instance.SetPlayerLaserVolume);
        enemyLaserSlider.onValueChanged.RemoveListener(SoundManager.Instance.SetEnemyLaserVolume);
        bossLaserSlider.onValueChanged.RemoveListener(SoundManager.Instance.SetBossLaserVolume);
        muteToggle.onValueChanged.RemoveListener(SoundManager.Instance.SetMuted);
    }
}