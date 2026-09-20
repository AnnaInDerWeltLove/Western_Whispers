using UnityEngine;
using UnityEngine.Video;
// KI Generiert
public class IntroManager : MonoBehaviour
{
    [SerializeField] private GameObject introPanel;
    [SerializeField] private VideoPlayer introVideoPlayer;
    [SerializeField] private Movement movement;
    [SerializeField] private TutorialManager tutorialManager;

    private static bool introAlreadyPlayed = false;

    private void Start()
    {
        if (introAlreadyPlayed)
        {
            introPanel.SetActive(false);
            return;
        }

        introAlreadyPlayed = true;

        movement.enabled = false;
        introPanel.SetActive(true);

        introVideoPlayer.prepareCompleted += OnVideoPrepared;
        introVideoPlayer.loopPointReached += OnIntroFinished;

        introVideoPlayer.Prepare();
    }

    private void OnVideoPrepared(VideoPlayer vp)
    {
        vp.Play();
    }

    private void OnIntroFinished(VideoPlayer vp)
    {
        introPanel.SetActive(false);
        movement.enabled = true;
        tutorialManager.ShowMovementTutorial();
    }
}