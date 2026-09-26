using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
// KI generirt
public class CreditsManager : MonoBehaviour
{
    [SerializeField] private GameObject creditsPanel;
    [SerializeField] private RectTransform creditsText;
    [SerializeField] private GameObject backButton;

    [Header("Credits Bewegung")]
    [SerializeField] private float scrollSpeed = 50f;
    [SerializeField] private float buttonDelay = 5f;
    
    [Header("Während Intro ausblenden / sperren")]
    [SerializeField] private GameObject playerInterface;
    [SerializeField] private CameraControl thirdPersonCameraControl;
    [SerializeField] private CameraControl firstPersonCameraControl;
    [SerializeField] private PlayerCameraSwitch playerCameraSwitch;
    [SerializeField] private WorldManager worldManager;
    [SerializeField] private UIInteractionController uiInteractionController;
    [SerializeField] private Movement movement;

    private bool creditsRunning;

    private void Start()
    {
        creditsPanel.SetActive(false);
        backButton.SetActive(false);
    }

    private void Update()
    {
        if (!creditsRunning)
        {
            return;
        }

        creditsText.anchoredPosition +=
            Vector2.up * scrollSpeed * Time.unscaledDeltaTime;
    }

    public void ShowCredits()
    {
        creditsPanel.SetActive(true);
        backButton.SetActive(false);
        DisablePlayerControls();

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        Time.timeScale = 0f;

        creditsRunning = true;

        StartCoroutine(ShowBackButtonAfterDelay());
    }

    private IEnumerator ShowBackButtonAfterDelay()
    {
        yield return new WaitForSecondsRealtime(buttonDelay);
        backButton.SetActive(true);
    }

    public void BackToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Test1");
    }
    
    private void DisablePlayerControls()
    {
        movement.enabled = false;
        thirdPersonCameraControl.enabled = false;
        firstPersonCameraControl.enabled = false;
        playerCameraSwitch.enabled = false;
        worldManager.enabled = false;
        uiInteractionController.enabled = false;

        playerInterface.SetActive(false);
    }
    
}