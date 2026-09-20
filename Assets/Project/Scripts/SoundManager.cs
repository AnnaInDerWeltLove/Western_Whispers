using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    [Header("Musik")]
    [SerializeField] private AudioClip introMusic;
    [SerializeField] private AudioClip gameMusic;
    [SerializeField] private AudioClip bossMusic;

    [Header("Laser-Soundeffekte")]
    [SerializeField] private AudioClip playerLaser;
    [SerializeField] private AudioClip enemyLaser;
    [SerializeField] private AudioClip bossLaser;

    [Header("Startlautstärken")]
    [Range(0f, 1f)]
    [SerializeField] private float musicVolume = 0.4f;

    [Range(0f, 1f)]
    [SerializeField] private float soundEffectVolume = 0.8f;

    [Range(0f, 1f)]
    [SerializeField] private float playerLaserVolume = 1f;

    [Range(0f, 1f)]
    [SerializeField] private float enemyLaserVolume = 1f;

    [Range(0f, 1f)]
    [SerializeField] private float bossLaserVolume = 1f;

    private AudioSource musicSource;
    private AudioSource soundEffectSource;

    private bool isMuted;

    public float MusicVolume => musicVolume;
    public float SoundEffectVolume => soundEffectVolume;
    public float PlayerLaserVolume => playerLaserVolume;
    public float EnemyLaserVolume => enemyLaserVolume;
    public float BossLaserVolume => bossLaserVolume;
    public bool IsMuted => isMuted;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        musicSource = gameObject.AddComponent<AudioSource>();
        musicSource.loop = true;
        musicSource.playOnAwake = false;
        musicSource.spatialBlend = 0f;

        soundEffectSource = gameObject.AddComponent<AudioSource>();
        soundEffectSource.playOnAwake = false;
        soundEffectSource.spatialBlend = 0f;

        LoadSettings();
        ApplySettings();
    }

    private void Start()
    {
        PlayIntroMusic();
    }
    
    public void PlayIntroMusic()
    {
        PlayMusic(introMusic);
    }

    public void PlayGameMusic()
    {
        PlayMusic(gameMusic);
    }

    public void PlayBossMusic()
    {
        PlayMusic(bossMusic);
    }

    private void PlayMusic(AudioClip newMusic)
    {
        if (newMusic == null)
        {
            return;
        }

        if (musicSource.clip == newMusic && musicSource.isPlaying)
        {
            return;
        }

        musicSource.Stop();
        musicSource.clip = newMusic;
        musicSource.Play();
    }

   
    public void PlayPlayerLaser()
    {
        PlaySoundEffect(playerLaser, playerLaserVolume);
    }

    public void PlayEnemyLaser()
    {
        PlaySoundEffect(enemyLaser, enemyLaserVolume);
    }

    public void PlayBossLaser()
    {
        PlaySoundEffect(bossLaser, bossLaserVolume);
    }

    private void PlaySoundEffect(AudioClip clip, float individualVolume)
    {
        if (clip == null || isMuted)
        {
            return;
        }

        soundEffectSource.PlayOneShot(clip, individualVolume);
    }
    
    public void SetMusicVolume(float volume)
    {
        musicVolume = Mathf.Clamp01(volume);
        musicSource.volume = musicVolume;
        SaveSettings();
    }

    public void SetSoundEffectVolume(float volume)
    {
        soundEffectVolume = Mathf.Clamp01(volume);
        soundEffectSource.volume = soundEffectVolume;
        SaveSettings();
    }

    public void SetPlayerLaserVolume(float volume)
    {
        playerLaserVolume = Mathf.Clamp01(volume);
        SaveSettings();
    }

    public void SetEnemyLaserVolume(float volume)
    {
        enemyLaserVolume = Mathf.Clamp01(volume);
        SaveSettings();
    }

    public void SetBossLaserVolume(float volume)
    {
        bossLaserVolume = Mathf.Clamp01(volume);
        SaveSettings();
    }

    public void SetMuted(bool muted)
    {
        isMuted = muted;
        ApplySettings();
        SaveSettings();
    }

    private void ApplySettings()
    {
        musicSource.volume = musicVolume;
        soundEffectSource.volume = soundEffectVolume;

        musicSource.mute = isMuted;
        soundEffectSource.mute = isMuted;
    }

    private void SaveSettings()
    {
        PlayerPrefs.SetFloat("MusicVolume", musicVolume);
        PlayerPrefs.SetFloat("SoundEffectVolume", soundEffectVolume);
        PlayerPrefs.SetFloat("PlayerLaserVolume", playerLaserVolume);
        PlayerPrefs.SetFloat("EnemyLaserVolume", enemyLaserVolume);
        PlayerPrefs.SetFloat("BossLaserVolume", bossLaserVolume);
        PlayerPrefs.SetInt("SoundMuted", isMuted ? 1 : 0);

        PlayerPrefs.Save();
    }

    private void LoadSettings()
    {
        musicVolume = PlayerPrefs.GetFloat("MusicVolume", musicVolume);

        soundEffectVolume = PlayerPrefs.GetFloat("SoundEffectVolume", soundEffectVolume);

        playerLaserVolume = PlayerPrefs.GetFloat("PlayerLaserVolume", playerLaserVolume);

        enemyLaserVolume = PlayerPrefs.GetFloat("EnemyLaserVolume", enemyLaserVolume);

        bossLaserVolume = PlayerPrefs.GetFloat("BossLaserVolume", bossLaserVolume);

        isMuted = PlayerPrefs.GetInt("SoundMuted", 0) == 1;
    }
}