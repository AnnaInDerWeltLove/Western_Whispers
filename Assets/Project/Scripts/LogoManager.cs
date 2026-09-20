using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
// KI generiert
public class LogoManager : MonoBehaviour
{
    [SerializeField] private float logoDuration = 2.5f;

    private void Start()
    {
        StartCoroutine(ShowLogo());
    }

    private IEnumerator ShowLogo()
    {
        yield return new WaitForSeconds(logoDuration);

        SceneManager.LoadScene("Test1");
    }
}