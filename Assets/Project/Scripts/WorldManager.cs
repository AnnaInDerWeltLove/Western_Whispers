using UnityEngine;

public class WorldManager : MonoBehaviour
{
    // ------------------------------------------------------------
    // ALLGEMEINE EINSTELLUNGEN
    // ------------------------------------------------------------

    [Header("Steuerung")]

    // Taste, mit der zwischen den beiden Welten gewechselt wird.
    // Sie kann später direkt im Inspector geändert werden.
    [SerializeField] private KeyCode switchKey = KeyCode.Q;


    // ------------------------------------------------------------
    // LICHTQUELLE
    // ------------------------------------------------------------

    [Header("Licht")]

    // Das Directional Light der Szene.
    // Dieses ziehen wir später im Inspector hier hinein.
    [SerializeField] private Light directionalLight;


    // ------------------------------------------------------------
    // NORMALWELT
    // ------------------------------------------------------------

    [Header("Normalwelt")]

    // Skybox-Material der normalen Welt.
    [SerializeField] private Material normalSkybox;

    // Farbe des Directional Lights in der normalen Welt.
    [SerializeField] private Color normalLightColor = Color.white;

    // Stärke des Directional Lights.
    [SerializeField] private float normalLightIntensity = 1f;

    // Stärke des Environment Lights.
    [SerializeField] private float normalEnvironmentIntensity = 1f;


    // ------------------------------------------------------------
    // GEISTERWELT
    // ------------------------------------------------------------

    [Header("Geisterwelt")]

    // Skybox-Material der Geisterwelt.
    [SerializeField] private Material spiritSkybox;

    // Farbe des Directional Lights in der Geisterwelt.
    [SerializeField] private Color spiritLightColor = Color.white;

    // Stärke des Directional Lights in der Geisterwelt.
    [SerializeField] private float spiritLightIntensity = 0.3f;

    // Stärke des Environment Lights in der Geisterwelt.
    [SerializeField] private float spiritEnvironmentIntensity = 0.3f;

    [SerializeField] private GhostTimeTimer ghostTimeTimer;
    // ------------------------------------------------------------
    // INTERNER ZUSTAND
    // ------------------------------------------------------------

    // Merkt sich, in welcher Welt wir uns gerade befinden.
    private bool isSpiritWorld = false;

    public static event System.Action<bool> OnWorldChanged;

    private void Start()
    {
        // Beim Spielstart wird immer die normale Welt geladen.
        SetNormalWorld();
    }


    private void Update()
    {
        // Prüft jeden Frame, ob die Wechsel-Taste gedrückt wurde.
        if (Input.GetKeyDown(switchKey))
        {
            SwitchWorld();
           
        }
    }


    // ------------------------------------------------------------
    // WELT WECHSELN
    // ------------------------------------------------------------

    private void SwitchWorld()
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


    // ------------------------------------------------------------
    // NORMALWELT AKTIVIEREN
    // ------------------------------------------------------------

    public void SetNormalWorld()
    {
        // Skybox wechseln.
        RenderSettings.skybox = normalSkybox;

        // Directional Light einstellen.
        directionalLight.color = normalLightColor;
        directionalLight.intensity = normalLightIntensity;

        // Environment Lighting einstellen.
        RenderSettings.ambientIntensity = normalEnvironmentIntensity;

        // Unity mitteilen, dass sich die Umgebungsbeleuchtung geändert hat.
        DynamicGI.UpdateEnvironment();

        // Zustand speichern.
        isSpiritWorld = false;
       
        // Informiert andere Scripts:
        // Die Normalwelt ist jetzt aktiv.
        OnWorldChanged?.Invoke(false);
    }


    // ------------------------------------------------------------
    // GEISTERWELT AKTIVIEREN
    // ------------------------------------------------------------

    private void SetSpiritWorld()
    {
        // Skybox wechseln.
        RenderSettings.skybox = spiritSkybox;

        // Directional Light einstellen.
        directionalLight.color = spiritLightColor;
        directionalLight.intensity = spiritLightIntensity;

        // Environment Lighting einstellen.
        RenderSettings.ambientIntensity = spiritEnvironmentIntensity;

        // Umgebungsbeleuchtung aktualisieren.
        DynamicGI.UpdateEnvironment();

        // Zustand speichern.
        isSpiritWorld = true;
       
        // Informiert andere Scripts:
        // Die Geisterwelt ist jetzt aktiv.
        OnWorldChanged?.Invoke(true);
    }
}