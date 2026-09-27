using TMPro;
using UnityEngine;

// KI Generiert
public class TutorialManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject tutorialPanel;
    [SerializeField] private TMP_Text tutorialText;

    private int tutorialStep = 0;

    private Vector3 lastMousePosition;

    private void Update()
    {
        switch (tutorialStep)
        {
            // 1. Mit der Maus umsehen
            case 0:
                if (Input.mousePosition != lastMousePosition)
                {
                    ShowWASDTutorial();
                    tutorialStep = 1;
                }
                break;


            // 2. Mit WASD bewegen
            case 1:
                if (Input.GetAxis("Horizontal") != 0 ||
                    Input.GetAxis("Vertical") != 0)
                {
                    ShowJumpTutorial();
                    tutorialStep = 2;
                }
                break;


            // 3. Springen
            case 2:
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    ShowPerspectiveTutorial();
                    tutorialStep = 3;
                }
                break;


            // 4. Perspektive wechseln
            case 3:
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    ShowUITutorial();
                    tutorialStep = 4;
                }
                break;


            // 5. UI öffnen
            case 4:
                if (Input.GetMouseButtonDown(1))
                {
                    ShowHoverTutorial();
                    tutorialStep = 5;
                }
                break;
        }
    }


    // Wird vom IntroManager aufgerufen
    public void ShowMovementTutorial()
    {
        if (PlayerPrefs.GetInt("TutorialCompleted", 0) == 1)
        {
            tutorialPanel.SetActive(false);
            tutorialStep = 6;
            return;
        }
        tutorialPanel.SetActive(true);

        tutorialText.text =
            "Schau dich um, indem du die Maus bewegst.";

        lastMousePosition = Input.mousePosition;
        tutorialStep = 0;
    }


    private void ShowWASDTutorial()
    {
        tutorialText.text =
            "Bewege dich mit den Tasten WASD.";
    }


    private void ShowJumpTutorial()
    {
        tutorialText.text =
            "Springe mit der Leertaste.";
    }


    private void ShowPerspectiveTutorial()
    {
        tutorialText.text =
            "Wechsle die Perspektive mit Q.";
    }


    private void ShowUITutorial()
    {
        tutorialText.text =
            "Drücke die rechte Maustaste,\num dein Menü zu öffnen.";
    }


    private void ShowHoverTutorial()
    {
        tutorialText.text =
            "Fahre mit der Maus über die Symbole,\num herauszufinden, was sie tun.";
        
    }
    
    
    public void CompleteTutorial()
    {
        tutorialPanel.SetActive(false);
        tutorialStep = 6;

        PlayerPrefs.SetInt("TutorialCompleted", 1);
        PlayerPrefs.Save();
    }
}