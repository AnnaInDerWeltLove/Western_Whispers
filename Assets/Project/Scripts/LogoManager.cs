using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
// KI generiert
public class LogoManager : MonoBehaviour
{
    [SerializeField] private CanvasGroup logoCanvasGroup;

    [Header("Zeiten")]
    [SerializeField] private float fadeDuration = 1f;
    [SerializeField] private float visibleDuration = 1.5f;
    [SerializeField] private RectTransform logoTransform;
    [SerializeField] private float startScale = 0.85f;
    [SerializeField] private GameObject secondImage;

    private void Start()
    {
        StartCoroutine(ShowLogo());
    }

    private IEnumerator ShowLogo()
    {
        // Unsichtbar starten
        logoCanvasGroup.alpha = 0f;

        // Einblenden
        float timer = 0f;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            float progress = timer / fadeDuration;
            logoCanvasGroup.alpha = Mathf.Lerp(0f, 1f, progress);
            float scale = Mathf.Lerp(startScale, 1f, progress);
            logoTransform.localScale = Vector3.one * scale;

            yield return null;
        }
        logoCanvasGroup.alpha = 1f;
        logoTransform.localScale = Vector3.one;
        secondImage.SetActive(true);

        // Logo stehen lassen
        yield return new WaitForSeconds(visibleDuration);

        SceneManager.LoadScene("Test1");
    }
    private void Awake()
    {
        logoCanvasGroup.alpha = 0f;
        logoTransform.localScale = new Vector3(startScale, startScale, 1f);
        secondImage.SetActive(false);
    }
}   
