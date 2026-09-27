using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
// KI generirt
public class CreditsManager : MonoBehaviour
{
    [SerializeField] private GameObject creditsPanel;
    [SerializeField] private RectTransform creditsText;
 
    [Header("Credits Bewegung")]
    [SerializeField] private float scrollSpeed = 50f;
    [SerializeField] private float endDelay = 2f;
    
    [Header("Während Intro ausblenden / sperren")]
    [SerializeField] private GameObject playerInterface;
    [SerializeField] private CameraControl thirdPersonCameraControl;
    [SerializeField] private CameraControl firstPersonCameraControl;
    [SerializeField] private PlayerCameraSwitch playerCameraSwitch;
    [SerializeField] private WorldManager worldManager;
    [SerializeField] private UIInteractionController uiInteractionController;
    [SerializeField] private Movement movement;
    
    [Header("Credits Übergang")]
    [SerializeField] private CanvasGroup fadePanel;
    [SerializeField] private float fadeDuration = 1.5f;
    [SerializeField] private float blackScreenDuration = 1f;

    private bool creditsRunning;

    private void Start()
    {
        creditsPanel.SetActive(false);
        fadePanel.alpha = 0f;
        fadePanel.blocksRaycasts = false;
        
    }

    private void Update()
    {
        if (!creditsRunning)
        {
            return;
        }

        creditsText.anchoredPosition +=
            Vector2.up * scrollSpeed * Time.unscaledDeltaTime;

        Vector3[] corners = new Vector3[4];
        creditsText.GetWorldCorners(corners);

// Unterkante des Credits-Textes
        float bottomEdge = corners[0].y;

// Oberkante des Bildschirms
        float screenTop = Screen.height;

        if (bottomEdge >= screenTop)
        {
            creditsRunning = false;
            StartCoroutine(EndCreditsAfterDelay());
        }
    }

    public void ShowCredits()
    {
        StartCoroutine(FadeToCredits());
    }

    private IEnumerator FadeToCredits()
    {
        fadePanel.blocksRaycasts = true;

        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.unscaledDeltaTime;

            fadePanel.alpha =
                Mathf.Clamp01(elapsedTime / fadeDuration);

            yield return null;
        }

        fadePanel.alpha = 1f;

        yield return new WaitForSecondsRealtime(blackScreenDuration);

        SoundManager.Instance.PlayCreditsMusic();

        creditsPanel.SetActive(true);
        DisablePlayerControls();

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        Time.timeScale = 0f;
        creditsRunning = true;
        

        fadePanel.alpha = 0f;
        fadePanel.blocksRaycasts = false;
    }
    
    private IEnumerator EndCreditsAfterDelay()
    {
        yield return new WaitForSecondsRealtime(endDelay);
        BackToMenu();
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