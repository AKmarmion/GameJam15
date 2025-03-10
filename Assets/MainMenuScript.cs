using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    
    [SerializeField] private GameObject creditsPanel; 

    public void StartGame()
    {
        
        SceneManager.LoadScene("StartLevel");
    }

    public void ShowCredits()
    {
        creditsPanel.SetActive(true);
    }

    public void HideCredits()
    {
        creditsPanel.SetActive(false);
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game!"); 
        Application.Quit(); 
    }
}
