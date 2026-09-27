using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

// KI-Generiert
public class UIInteractionController : MonoBehaviour
{
    [Header("Spielsteuerung")]
    [SerializeField] private CameraControl mainCameraControl;
    [SerializeField] private CameraControl fpCameraControl;
    [SerializeField] private PlayerCameraSwitch playerCameraSwitch;

    [Header("Spielfunktionen")]
    [SerializeField] private Inventory inventory;
    [SerializeField] private WorldManager worldManager;

    [Header("Interface Fenster")]
    [SerializeField] private GameObject progressPanel;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject settingsPanel;

    [Header("Hover")]
    [SerializeField] private GameObject hoverPanel;
    [SerializeField] private TMP_Text hoverText;

    [Header("Tutorial")]
    [SerializeField] private TutorialManager tutorialManager;

    [Header("Szenen")]
    [SerializeField] private string mainMenuScene = "Menü";

    private bool uiModeActive;
    private bool firstHoverDone;


    private void Start()
    {
        CloseAllWindows();

        if (hoverPanel != null)
            hoverPanel.SetActive(false);

        CloseUI();
    }


    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (uiModeActive)
                CloseUI();
            else
                OpenUI();
        }
    }


    // -------------------------
    // UI MODUS
    // -------------------------

    public void OpenUI()
    {
        uiModeActive = true;

        Time.timeScale = 0f;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        mainCameraControl.enabled = false;
        fpCameraControl.enabled = false;
        playerCameraSwitch.enabled = false;
    }


    public void CloseUI()
    {
        CloseAllWindows();

        uiModeActive = false;

        Time.timeScale = 1f;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        mainCameraControl.enabled = true;
        fpCameraControl.enabled = true;
        playerCameraSwitch.enabled = true;
    }


    // -------------------------
    // INVENTAR
    // -------------------------

    public void OpenInventory()
    {
        CloseAllWindows();
        inventory.OpenInventory();
    }


    // -------------------------
    // FORTSCHRITT / KARTE
    // -------------------------

    public void OpenProgress()
    {
        CloseAllWindows();

        if (progressPanel != null)
            progressPanel.SetActive(true);
    }


    // -------------------------
    // PAUSE
    // -------------------------

    public void OpenPause()
    {
        CloseAllWindows();

        if (pausePanel != null)
            pausePanel.SetActive(true);
    }


    public void OpenSettings()
    {
        CloseAllWindows();

        if (settingsPanel != null)
            settingsPanel.SetActive(true);
    }


    public void BackToPause()
    {
        CloseAllWindows();

        if (pausePanel != null)
            pausePanel.SetActive(true);
    }


    public void BackToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(mainMenuScene);
    }


    // -------------------------
    // WELTENWECHSEL
    // -------------------------

    public void SwitchWorld()
    {
        worldManager.SwitchWorld();
    }


    // -------------------------
    // FENSTER SCHLIESSEN
    // -------------------------

    public void CloseCurrentWindow()
    {
        CloseAllWindows();
    }


    private void CloseAllWindows()
    {
        if (inventory != null)
            inventory.CloseInventory();

        if (progressPanel != null)
            progressPanel.SetActive(false);

        if (pausePanel != null)
            pausePanel.SetActive(false);

        if (settingsPanel != null)
            settingsPanel.SetActive(false);
    }


    // -------------------------
    // HOVER
    // -------------------------

    public void ShowHoverText(string text)
    {
        if (hoverPanel == null || hoverText == null)
            return;

        hoverText.text = text;
        hoverPanel.SetActive(true);

        if (!firstHoverDone)
        {
            firstHoverDone = true;

            if (tutorialManager != null)
                tutorialManager.CompleteTutorial();
        }
    }


    public void HideHoverText()
    {
        if (hoverPanel != null)
            hoverPanel.SetActive(false);
    }
}