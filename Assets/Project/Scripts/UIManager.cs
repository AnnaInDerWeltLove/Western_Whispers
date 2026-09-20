using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
/*public class UIManager : MonoBehaviour
{   
    [Header("Player")]
    [SerializeField] private Player player;
    [SerializeField] private Image playerHealthBar;
    
    [Header("Boss")]
    [SerializeField] private Enemy boss;
    [SerializeField] private Image bossHealthBar;
    
    [Header("Panels")]
    [SerializeField] private GameObject startPanel;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject settingsPanel;
    
    [Header("Sonstiges")]
    [SerializeField] private Camera gameplayCamera;
   
    private float previousTimeScale;
    
    private void Start()
    {
        Time.timeScale = 0f;

        if (startPanel != null)
        {
            startPanel.SetActive(true);
        }
        if (winPanel != null)
        {
            winPanel.SetActive(false);
        }
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }
        if (settingsPanel != null)
        {
            settingsPanel.SetActive(false);
        }
    }
    
    
    private void Update()
    {
        if (player != null && playerHealthBar != null)
        {
            float playerHealthPercentage = (float)player.CurrentHealth / player.MaxHealth;
            playerHealthBar.fillAmount = playerHealthPercentage;
        }

        if (boss != null && bossHealthBar != null && gameplayCamera != null)
        {
            Vector3 vieportPosition = gameplayCamera.WorldToViewportPoint(boss.transform.position);
            bool bossIsVisible = vieportPosition.z > 0f && vieportPosition.x >= 0f && vieportPosition.x <= 1f && vieportPosition.y >= 0f && vieportPosition.y <= 1f;
            bossHealthBar.gameObject.SetActive(bossIsVisible);
            
            if(bossIsVisible)
            {
                 float bossHealthPercentage = (float)boss.CurrentHealth / boss.MaxHealth;
                 bossHealthBar.fillAmount = bossHealthPercentage;
            }
            
        }
    }

    public void StartGame()
    {
        Time.timeScale = 1f;
        SoundManager.Instance?.PlayGameMusic();
        
        if (startPanel != null)
        {
            startPanel.SetActive(false);
        }

        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }
    }

    public void ShowGameOver()
    {
        Time.timeScale = 0f;
        
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }
    }

    public void ShowWinScreen()
    {
        Time.timeScale = 0f;
        
        if (winPanel != null)
        {
            winPanel.SetActive(true);
        }
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SoundManager.Instance?.PlayIntroMusic();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void OpenSettings()
    {
        previousTimeScale = Time.timeScale;
        Time.timeScale = 0f;

        if (settingsPanel != null)
        {
            settingsPanel.SetActive(true);
        }
    }

    public void CloseSettings()
    {
        if (settingsPanel != null)
        {
            settingsPanel.SetActive(false);
        }

        Time.timeScale = previousTimeScale;
    }
    
    public void SetBoss(Enemy newBoss)
    {
        boss = newBoss;

        if (bossHealthBar != null)
        {
            bossHealthBar.fillAmount = 1f;
            bossHealthBar.gameObject.SetActive(false);
        }
    }
}
*/