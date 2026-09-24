using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
// KI-Generiert

public class MenuScene : MonoBehaviour
{
    [Header("Kameras")]
    [SerializeField] private Camera backgroundCamera;
    [SerializeField] private Camera menuCamera;
    [SerializeField] private float moveDuration = 2f;

    [Header("Menü")]
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private CanvasGroup menuCanvasGroup;

    [Header("Untermenüs")]
    [SerializeField] private GameObject fortschrittPanel;
    [SerializeField] private GameObject einstellungPanel;
    [SerializeField] private Toggle fullscreenToggle;
    
    [Header("Fortschritt")]
    [SerializeField] private Button saloonButton;

    [Header("Hauptmenü-Buttons")]
    [SerializeField] private Button[] mainMenuButtons;

    [Header("Licht")]
    [SerializeField] private GameObject lightOn;
    [SerializeField] private GameObject lightOff;
    [SerializeField] private Light directionalLight;
    [SerializeField] private float normalLightIntensity = 1f;
    [SerializeField] private float darkLightIntensity = 0.15f;
    
    [Header("Sound")]
    [SerializeField] private SoundManager soundManager;
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private Toggle soundToggle;

    [Header("Übergang")]
    [SerializeField] private GameObject blackScreen;

    // Ursprüngliche Kameraeinstellungen
    private Vector3 backgroundStartPosition;
    private Quaternion backgroundStartRotation;

    private float backgroundStartFOV;
    private float backgroundStartNearClip;
    private float backgroundStartFarClip;

    private void Start()
    {
        // Ursprüngliche Werte speichern
        backgroundStartPosition = backgroundCamera.transform.position;
        backgroundStartRotation = backgroundCamera.transform.rotation;

        backgroundStartFOV = backgroundCamera.fieldOfView;
        backgroundStartNearClip = backgroundCamera.nearClipPlane;
        backgroundStartFarClip = backgroundCamera.farClipPlane;

        backgroundCamera.gameObject.SetActive(true);
        menuCamera.gameObject.SetActive(false);

        menuCanvasGroup.interactable = false;
        menuCanvasGroup.blocksRaycasts = false;
        bool fullscreen = PlayerPrefs.GetInt("Fullscreen", Screen.fullScreen ? 1 : 0) == 1;

        Screen.fullScreen = fullscreen; 
        fullscreenToggle.SetIsOnWithoutNotify(fullscreen);
        float volume = PlayerPrefs.GetFloat("MasterVolume", 1f);

        volumeSlider.SetValueWithoutNotify(soundManager.MusicVolume);
        soundToggle.SetIsOnWithoutNotify(!soundManager.IsMuted);
        UpdateProgressMenu();

        blackScreen.SetActive(false);

        StartCoroutine(MoveToMenu());
    }

    private IEnumerator MoveToMenu()
    {
        Vector3 startPosition = backgroundCamera.transform.position;
        Quaternion startRotation = backgroundCamera.transform.rotation;

        Vector3 targetPosition = menuCamera.transform.position;
        Quaternion targetRotation = menuCamera.transform.rotation;

        // Kameraeinstellungen für den Übergang
        float startFOV = backgroundCamera.fieldOfView;
        float targetFOV = menuCamera.fieldOfView;

        float startNear = backgroundCamera.nearClipPlane;
        float targetNear = menuCamera.nearClipPlane;

        float startFar = backgroundCamera.farClipPlane;
        float targetFar = menuCamera.farClipPlane;

        float time = 0f;

        while (time < moveDuration)
        {
            time += Time.deltaTime;

            float progress = Mathf.Clamp01(time / moveDuration);

            // Sanftes Beschleunigen und Abbremsen
            progress = Mathf.SmoothStep(0f, 1f, progress);

            // Position
            backgroundCamera.transform.position =
                Vector3.Lerp(startPosition, targetPosition, progress);

            // Rotation
            backgroundCamera.transform.rotation =
                Quaternion.Slerp(startRotation, targetRotation, progress);

            // Field of View
            backgroundCamera.fieldOfView =
                Mathf.Lerp(startFOV, targetFOV, progress);

            // Clipping Planes
            backgroundCamera.nearClipPlane =
                Mathf.Lerp(startNear, targetNear, progress);

            backgroundCamera.farClipPlane =
                Mathf.Lerp(startFar, targetFar, progress);

            yield return null;
        }

        // Exakte Zielwerte sicherstellen
        backgroundCamera.transform.position = targetPosition;
        backgroundCamera.transform.rotation = targetRotation;

        backgroundCamera.fieldOfView = targetFOV;
        backgroundCamera.nearClipPlane = targetNear;
        backgroundCamera.farClipPlane = targetFar;

        // Erst jetzt zur Menükamera wechseln
        menuCamera.gameObject.SetActive(true);
        backgroundCamera.gameObject.SetActive(false);

        menuCanvasGroup.interactable = true;
        menuCanvasGroup.blocksRaycasts = true;

        menuPanel.SetActive(true);
    }

    public void StartGameSequence()
    {
        menuCanvasGroup.interactable = false;
        menuCanvasGroup.blocksRaycasts = false;

        // Gleiche Ausgangswerte wie die Menükamera
        backgroundCamera.transform.position = menuCamera.transform.position;
        backgroundCamera.transform.rotation = menuCamera.transform.rotation;

        backgroundCamera.fieldOfView = menuCamera.fieldOfView;
        backgroundCamera.nearClipPlane = menuCamera.nearClipPlane;
        backgroundCamera.farClipPlane = menuCamera.farClipPlane;

        backgroundCamera.gameObject.SetActive(true);
        menuCamera.gameObject.SetActive(false);

        StartCoroutine(ReturnAndStartGame());
    }

    private IEnumerator ReturnAndStartGame()
    {
        Vector3 startPosition = backgroundCamera.transform.position;
        Quaternion startRotation = backgroundCamera.transform.rotation;

        float startFOV = backgroundCamera.fieldOfView;
        float startNear = backgroundCamera.nearClipPlane;
        float startFar = backgroundCamera.farClipPlane;

        float time = 0f;

        while (time < moveDuration)
        {
            time += Time.deltaTime;

            float progress = Mathf.Clamp01(time / moveDuration);
            progress = Mathf.SmoothStep(0f, 1f, progress);

            // Zur ursprünglichen Position zurückfahren
            backgroundCamera.transform.position =
                Vector3.Lerp(
                    startPosition,
                    backgroundStartPosition,
                    progress
                );

            backgroundCamera.transform.rotation =
                Quaternion.Slerp(
                    startRotation,
                    backgroundStartRotation,
                    progress
                );

            // Ursprüngliche Kameraeinstellungen wiederherstellen
            backgroundCamera.fieldOfView =
                Mathf.Lerp(startFOV, backgroundStartFOV, progress);

            backgroundCamera.nearClipPlane =
                Mathf.Lerp(startNear, backgroundStartNearClip, progress);

            backgroundCamera.farClipPlane =
                Mathf.Lerp(startFar, backgroundStartFarClip, progress);

            yield return null;
        }

        // Exakte Ausgangswerte
        backgroundCamera.transform.position = backgroundStartPosition;
        backgroundCamera.transform.rotation = backgroundStartRotation;

        backgroundCamera.fieldOfView = backgroundStartFOV;
        backgroundCamera.nearClipPlane = backgroundStartNearClip;
        backgroundCamera.farClipPlane = backgroundStartFarClip;

        yield return StartCoroutine(BlinkLight());

        directionalLight.intensity = 0f;
        blackScreen.SetActive(true);

        yield return new WaitForSeconds(2f);

        UnityEngine.SceneManagement.SceneManager.LoadScene("Level1Test");
    }

    private IEnumerator BlinkLight()
    {
        lightOn.SetActive(false);
        lightOff.SetActive(true);
        directionalLight.intensity = darkLightIntensity;

        yield return new WaitForSeconds(0.5f);

        lightOn.SetActive(true);
        lightOff.SetActive(false);
        directionalLight.intensity = normalLightIntensity;

        yield return new WaitForSeconds(0.30f);

        lightOn.SetActive(false);
        lightOff.SetActive(true);
        directionalLight.intensity = darkLightIntensity;

        yield return new WaitForSeconds(0.4f);
        
        lightOn.SetActive(true);
        lightOff.SetActive(false);
        directionalLight.intensity = normalLightIntensity;

        yield return new WaitForSeconds(0.20f);
        
        lightOn.SetActive(false);
        lightOff.SetActive(true);
        directionalLight.intensity = darkLightIntensity;

        yield return new WaitForSeconds(0.1f);
    }
    public void OpenFortschritt()
    {
        ResetMenuButtons();
        SetMainButtonsInteractable(false);

        einstellungPanel.SetActive(false);
        fortschrittPanel.SetActive(true);
    }

    public void OpenEinstellungen()
    {
        ResetMenuButtons();
        SetMainButtonsInteractable(false);

        fortschrittPanel.SetActive(false);
        einstellungPanel.SetActive(true);
    }

    public void BackToMainMenu()
    {
        fortschrittPanel.SetActive(false);
        einstellungPanel.SetActive(false);

        SetMainButtonsInteractable(true);
        ResetMenuButtons();
    }

    private void SetMainButtonsInteractable(bool interactable)
    {
        foreach (Button button in mainMenuButtons)
        {
            if (button == null)
                continue;

            button.interactable = interactable;
            button.gameObject.SetActive(interactable);
        }
    }

    private void ResetMenuButtons()
    {
        if (EventSystem.current == null)
            return;

        EventSystem.current.SetSelectedGameObject(null);

        PointerEventData eventData =
            new PointerEventData(EventSystem.current);

        foreach (Button button in mainMenuButtons)
        {
            if (button == null)
                continue;

            button.OnPointerExit(eventData);

            // Animierte Buttons in ihren Ausgangszustand versetzen.
            if (button.transition == Selectable.Transition.Animation &&
                button.animator != null)
            {
                button.animator.Rebind();
                button.animator.Update(0f);
            }
        }
    }
    public void SetVolume(float volume)
    {
        soundManager.SetMusicVolume(volume);
        soundManager.SetSoundEffectVolume(volume);
    }
    public void SetFullscreen(bool fullscreen)
    {
        Screen.fullScreen = fullscreen;

        PlayerPrefs.SetInt("Fullscreen", fullscreen ? 1 : 0);
        PlayerPrefs.Save();
    }
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Spiel wird beendet");
    }
    public void SetSoundEnabled(bool soundEnabled)
    {
        soundManager.SetMuted(!soundEnabled);
    }
    private void UpdateProgressMenu()
    {
        int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 0);

        if (unlockedLevel >= 1)
        {
            saloonButton.targetGraphic.color = Color.white;
            saloonButton.interactable = true;
        }
        else
        {
            saloonButton.interactable = false;
        }
    }
}