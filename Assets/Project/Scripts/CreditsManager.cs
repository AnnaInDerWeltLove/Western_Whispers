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
}