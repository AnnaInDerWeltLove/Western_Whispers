using System.Collections;
using UnityEngine;
// KI-Generiert
public class MenuCameraSequence : MonoBehaviour
{
    [Header("Kameras")]
    [SerializeField] private Camera backgroundCamera;
    [SerializeField] private Camera menuCamera;
    [SerializeField] private float moveDuration = 2f;

    [Header("Menü")]
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private CanvasGroup menuCanvasGroup;

    [Header("Licht")]
    [SerializeField] private GameObject lightOn;
    [SerializeField] private GameObject lightOff;
    [SerializeField] private Light directionalLight;
    [SerializeField] private float normalLightIntensity = 1f;
    [SerializeField] private float darkLightIntensity = 0.15f;

    [Header("Übergang")]
    [SerializeField] private GameObject blackScreen;

    private Vector3 backgroundStartPosition;
    private Quaternion backgroundStartRotation;

    private void Start()
    {
        backgroundStartPosition = backgroundCamera.transform.position;
        backgroundStartRotation = backgroundCamera.transform.rotation;

        backgroundCamera.gameObject.SetActive(true);
        menuCamera.gameObject.SetActive(false);

        menuCanvasGroup.interactable = false;
        menuCanvasGroup.blocksRaycasts = false;
        blackScreen.SetActive(false);

        StartCoroutine(MoveToMenu());
    }

    private IEnumerator MoveToMenu()
    {
        Vector3 startPosition = backgroundCamera.transform.position;
        Quaternion startRotation = backgroundCamera.transform.rotation;

        Vector3 targetPosition = menuCamera.transform.position;
        Quaternion targetRotation = menuCamera.transform.rotation;

        float time = 0f;

        while (time < moveDuration)
        {
            time += Time.deltaTime;
            float progress = time / moveDuration;

            backgroundCamera.transform.position =
                Vector3.Lerp(startPosition, targetPosition, progress);

            backgroundCamera.transform.rotation =
                Quaternion.Slerp(startRotation, targetRotation, progress);

            yield return null;
        }

        backgroundCamera.transform.position = targetPosition;
        backgroundCamera.transform.rotation = targetRotation;

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

        backgroundCamera.transform.position = menuCamera.transform.position;
        backgroundCamera.transform.rotation = menuCamera.transform.rotation;

        backgroundCamera.gameObject.SetActive(true);
        menuCamera.gameObject.SetActive(false);
        

        StartCoroutine(ReturnAndStartGame());
    }

    private IEnumerator ReturnAndStartGame()
    {
        Vector3 startPosition = backgroundCamera.transform.position;
        Quaternion startRotation = backgroundCamera.transform.rotation;

        float time = 0f;

        while (time < moveDuration)
        {
            time += Time.deltaTime;
            float progress = time / moveDuration;

            backgroundCamera.transform.position =
                Vector3.Lerp(startPosition, backgroundStartPosition, progress);

            backgroundCamera.transform.rotation =
                Quaternion.Slerp(startRotation, backgroundStartRotation, progress);

            yield return null;
        }

        backgroundCamera.transform.position = backgroundStartPosition;
        backgroundCamera.transform.rotation = backgroundStartRotation;

        yield return StartCoroutine(BlinkLight());
        directionalLight.intensity = 0f;
        blackScreen.SetActive(true);
        
        yield return new WaitForSeconds(2f);

        UnityEngine.SceneManagement.SceneManager.LoadScene("Level1");
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

        yield return new WaitForSeconds(1f);
    }
}