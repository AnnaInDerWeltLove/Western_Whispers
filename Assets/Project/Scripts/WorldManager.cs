using UnityEngine;
//KI generiert
public class WorldManager : MonoBehaviour
{
    // Verwendung von Headern zur Übersichtlichkeit
    
    [Header("Licht")]
    [SerializeField] private Light directionalLight;

    [Header("Normalwelt")]
    [SerializeField] private Material normalSkybox;
    [SerializeField] private Color normalLightColor = Color.white;
    [SerializeField] private float normalLightIntensity = 1f;
    [SerializeField] private float normalEnvironmentIntensity = 1f;

    [Header("Geisterwelt")]
    [SerializeField] private Material spiritSkybox;
    [SerializeField] private Color spiritLightColor = Color.white;
    [SerializeField] private float spiritLightIntensity = 0.3f;
    [SerializeField] private float spiritEnvironmentIntensity = 0.3f;
    [SerializeField] private GhostTimeTimer ghostTimeTimer;
    
    private bool isSpiritWorld = false;
    public static event System.Action<bool> OnWorldChanged;
   // Startet das Spiel immer in der Normalen Welt
    private void Start()
    {
        SetNormalWorld();
    }
    
    // Auslösen des Weltenwechsels
    public void SwitchWorld()
    {
        if (isSpiritWorld)
        {
            SetNormalWorld();
            ghostTimeTimer.StopGhostTime();
        }
        else
        {
            SetSpiritWorld();
            ghostTimeTimer.StartGhostTime();
        }
    }
    // Weltenwechsel durch Licht und Skyboxveränderungen
    public void SetNormalWorld()
    {
        RenderSettings.skybox = normalSkybox;
        directionalLight.color = normalLightColor;
        directionalLight.intensity = normalLightIntensity;
        RenderSettings.ambientIntensity = normalEnvironmentIntensity;
        DynamicGI.UpdateEnvironment();
        isSpiritWorld = false;
        SoundManager.Instance.PlayNormalWorldMusic();
        OnWorldChanged?.Invoke(false);
    }

    private void SetSpiritWorld()
    {
        RenderSettings.skybox = spiritSkybox;
        directionalLight.color = spiritLightColor;
        directionalLight.intensity = spiritLightIntensity;
        RenderSettings.ambientIntensity = spiritEnvironmentIntensity;
        DynamicGI.UpdateEnvironment();
        isSpiritWorld = true;
        SoundManager.Instance.PlaySpiritWorldMusic();
        OnWorldChanged?.Invoke(true);
    }
}