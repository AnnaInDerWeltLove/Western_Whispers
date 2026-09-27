using UnityEngine;

// KI generiert
public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    [Header("Musik")]
    [SerializeField] private AudioClip logoMusic;
    [SerializeField] private AudioClip menuMusic;
    [SerializeField] private AudioClip normalWorldMusic;
    [SerializeField] private AudioClip spiritWorldMusic;
    [SerializeField] private AudioClip fightMusic;
    [SerializeField] private AudioClip creditsMusic;

    [Header("Soundeffekte")]
    [SerializeField] private AudioClip footstepsSound;
    [SerializeField] private AudioClip shotSound;

    [Header("Individuelle Lautstärken")]
    [Range(0f, 1f)]
    [SerializeField] private float logoVolume = 1f;

    [Range(0f, 1f)]
    [SerializeField] private float menuVolume = 1f;

    [Range(0f, 1f)]
    [SerializeField] private float normalWorldVolume = 1f;

    [Range(0f, 1f)]
    [SerializeField] private float spiritWorldVolume = 1f;

    [Range(0f, 1f)]
    [SerializeField] private float fightVolume = 1f;

    [Range(0f, 1f)]
    [SerializeField] private float footstepsVolume = 1f;

    [Range(0f, 1f)]
    [SerializeField] private float shotVolume = 1f;
    
    [Range(0f, 1f)]
    [SerializeField] private float creditsVolume = 1f;

    [Header("Spieler-Einstellung")]
    [Range(0f, 1f)]
    [SerializeField] private float masterVolume = 1f;

    private AudioSource musicSource;
    private AudioSource soundEffectSource;

    private bool isMuted;

    public float MasterVolume => masterVolume;
    public bool IsMuted => isMuted;

    private float currentMusicIndividualVolume = 1f;

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

    // -------------------------
    // MUSIK
    // -------------------------

    public void PlayLogoMusic()
    {
        PlayMusic(logoMusic, logoVolume);
    }

    public void PlayMenuMusic()
    {
        PlayMusic(menuMusic, menuVolume);
    }

    public void PlayNormalWorldMusic()
    {
        PlayMusic(normalWorldMusic, normalWorldVolume);
    }

    public void PlaySpiritWorldMusic()
    {
        PlayMusic(spiritWorldMusic, spiritWorldVolume);
    }

    public void PlayFightMusic()
    {
        PlayMusic(fightMusic, fightVolume);
    }
    
    public void PlayCreditsMusic()
    {
        PlayMusic(creditsMusic, creditsVolume);
    }

    private void PlayMusic(AudioClip newMusic, float individualVolume)
    {
        if (newMusic == null)
            return;

        currentMusicIndividualVolume = individualVolume;

        if (musicSource.clip == newMusic && musicSource.isPlaying)
        {
            ApplySettings();
            return;
        }

        musicSource.Stop();
        musicSource.clip = newMusic;
        ApplySettings();
        musicSource.Play();
    }

    // -------------------------
    // SOUNDEFFEKTE
    // -------------------------

    public void PlayFootsteps()
    {
        PlaySoundEffect(footstepsSound, footstepsVolume);
    }

    public void PlayShot()
    {
        PlaySoundEffect(shotSound, shotVolume);
    }

    private void PlaySoundEffect(AudioClip clip, float individualVolume)
    {
        if (clip == null || isMuted)
            return;

        soundEffectSource.PlayOneShot(
            clip,
            masterVolume * individualVolume
        );
    }

    // -------------------------
    // EINSTELLUNGEN
    // -------------------------

    public void SetMasterVolume(float volume)
    {
        masterVolume = Mathf.Clamp01(volume);
        ApplySettings();
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
        musicSource.volume =
            masterVolume * currentMusicIndividualVolume;

        musicSource.mute = isMuted;
        soundEffectSource.mute = isMuted;
    }

    private void SaveSettings()
    {
        PlayerPrefs.SetFloat("MasterVolume", masterVolume);
        PlayerPrefs.SetInt("SoundMuted", isMuted ? 1 : 0);
        PlayerPrefs.Save();
    }

    private void LoadSettings()
    {
        masterVolume =
            PlayerPrefs.GetFloat("MasterVolume", masterVolume);

        isMuted =
            PlayerPrefs.GetInt("SoundMuted", 0) == 1;
    }
    
    public void StopMusic()
    {
        musicSource.Stop();
    }
}