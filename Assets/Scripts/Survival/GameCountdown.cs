using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameCountdown : MonoBehaviour
{
    // References the time left text object and its TextMeshProUGUI component.
    [SerializeField] private GameObject _timeLeftText;
    private TextMeshProUGUI _timeLeftTextUGUI;

    // Tracks how much time has passed.
    private float _timeElapsed = 0;

    private void Start()
    {
        _timeLeftTextUGUI = _timeLeftText.GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        // Updates the timer constantly.
        _timeElapsed += Time.deltaTime;
        _timeLeftTextUGUI.text = $"{(int)_timeElapsed}/300s";
        if (_timeElapsed >= 300)
        {
            SceneManager.LoadScene("Win");
        }
    }
}