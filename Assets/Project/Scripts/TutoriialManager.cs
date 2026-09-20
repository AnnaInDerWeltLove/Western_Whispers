using TMPro;
using UnityEngine;
// KI Generiert
public class TutorialManager : MonoBehaviour
{
    [SerializeField] private GameObject tutorialPanel;
    [SerializeField] private TMP_Text tutorialText;

    private bool movementDone;
    private bool worldSwitchDone;
    private bool interactionDone;
    private bool fightDone;
    
    private void Update()
    {
        if (!movementDone)
        {
            if (Input.GetAxis("Horizontal") != 0 ||
                Input.GetAxis("Vertical") != 0)
            {
                CompleteMovementTutorial();
                ShowWorldSwitchTutorial();
            }
        }
        else if (!worldSwitchDone)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                CompleteWorldSwitchTutorial();
            }
        }
    }

    public void ShowMovementTutorial()
    {
        if (movementDone) return;

        tutorialPanel.SetActive(true);
        tutorialText.text = "WASD – Bewegen";
    }

    public void ShowWorldSwitchTutorial()
    {
        if (worldSwitchDone) return;

        tutorialPanel.SetActive(true);
        tutorialText.text = "Q – Zwischen den Welten wechseln";
    }

    public void ShowInteractionTutorial()
    {
        if (interactionDone) return;

        tutorialPanel.SetActive(true);
        tutorialText.text = "E – Interagieren";
    }

    public void ShowFightTutorial()
    {
        if (fightDone) return;

        tutorialPanel.SetActive(true);
        tutorialText.text = "Linksklick – Lichtpistole abfeuern";
    }

    public void CompleteMovementTutorial()
    {
        movementDone = true;
        tutorialPanel.SetActive(false);
    }

    public void CompleteWorldSwitchTutorial()
    {
        worldSwitchDone = true;
        tutorialPanel.SetActive(false);
    }

    public void CompleteInteractionTutorial()
    {
        interactionDone = true;
        tutorialPanel.SetActive(false);
    }

    public void CompleteFightTutorial()
    {
        fightDone = true;
        tutorialPanel.SetActive(false);
    }
}