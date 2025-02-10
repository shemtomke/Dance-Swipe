using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] Text followersText;
    [SerializeField] Button homeButton;

    SocialMetricsManager metricsManager;
    private void Start()
    {
        metricsManager = FindFirstObjectByType<SocialMetricsManager>();

        homeButton.onClick.AddListener(GoToHome);
    }
    private void Update()
    {
        // Get the likes to get the followers to update
        followersText.text = metricsManager.GetCurrentFollowers().ToString();
    }
    void GoToHome()
    {
        // Reset Scene
        SceneManager.LoadScene(0);
    }
}
