using UnityEngine;
using UnityEngine.SceneManagement;

// General menu handler.
public class MenuHandler : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Survival");
    }
}
