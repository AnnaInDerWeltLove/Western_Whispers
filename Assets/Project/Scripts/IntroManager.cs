using UnityEngine;
using UnityEngine.Video;
// KI Generiert
public class IntroManager : MonoBehaviour
{
    [SerializeField] private GameObject introPanel;
    [SerializeField] private VideoPlayer introVideoPlayer;
    [SerializeField] private Movement movement;
    [SerializeField] private TutorialManager tutorialManager;
    
    [Header("Während Intro ausblenden / sperren")]
    [SerializeField] private GameObject playerInterface;
    [SerializeField] private CameraControl thirdPersonCameraControl;
    [SerializeField] private CameraControl firstPersonCameraControl;
    [SerializeField] private PlayerCameraSwitch playerCameraSwitch;
    [SerializeField] private WorldManager worldManager;
    [SerializeField] private UIInteractionController uiInteractionController;

    private static bool introAlreadyPlayed = false;

    private void Start()
    {
        if (introAlreadyPlayed)
        {
            introPanel.SetActive(false);
            EnablePlayerControls();
            return;
        }
        DisablePlayerControls();
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
        EnablePlayerControls();
        movement.enabled = true;
        tutorialManager.ShowMovementTutorial();
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

    private void EnablePlayerControls()
    {
        movement.enabled = true;
        thirdPersonCameraControl.enabled = true;
        firstPersonCameraControl.enabled = true;
        playerCameraSwitch.enabled = true;
        worldManager.enabled = true;
        uiInteractionController.enabled = true;
        playerInterface.SetActive(true);
    }
}