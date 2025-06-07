using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

// General menu handler.
public class MenuHandler : MonoBehaviour
{
    // Easier data.
    [System.Serializable] private class ThreatExplanation
    {
        [SerializeField] private string _threat;
        [TextArea] [SerializeField] private string _explanation;

        // Getters.
        public string Threat => _threat;
        public string Explanation => _explanation;
    }

    // Stores the game threat explanations.
    [SerializeField] private List<ThreatExplanation> _threatExplanations;

    // References the [MainContentArranger] object.
    [SerializeField] private GameObject _mainContentArranger;

    // References the [GuideContent] object.
    [SerializeField] private GameObject _guideContent;

    // References the ThreatName object and its TextMeshProUGUI component.
    [SerializeField] private GameObject _threatName;
    private TextMeshProUGUI _threatNameUGUI;

    // References the ThreatExplanation object and its TextMeshProUGUI component.
    [SerializeField] private GameObject _threatExplanation;
    private TextMeshProUGUI _threatExplanationUGUI;

    // Tracks which threat explanation the player is looking at.
    private int _threatExplanationIndex = 0;

    private void Start()
    {
        _threatNameUGUI = _threatName.GetComponent<TextMeshProUGUI>();
        _threatExplanationUGUI = _threatExplanation.GetComponent<TextMeshProUGUI>();
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Survival");
    }

    public void EnableThreatExplanations()
    {
        _threatExplanationIndex = 0;
        _mainContentArranger.SetActive(false);
        _guideContent.SetActive(true);
        UpdateThreatExplanationText();
    }

    public void NavigateThreatExplanations(int increment)
    {
        _threatExplanationIndex = Mathf.Clamp(
            _threatExplanationIndex + increment,
            0,
            _threatExplanations.Count - 1
        );
        UpdateThreatExplanationText();
    }

    public void UpdateThreatExplanationText()
    {
        _threatNameUGUI.text = _threatExplanations[_threatExplanationIndex].Threat;
        _threatExplanationUGUI.text = _threatExplanations[_threatExplanationIndex].Explanation;
    }

    public void DisableThreatExplanations()
    {
        _mainContentArranger.SetActive(true);
        _guideContent.SetActive(false);
    }
}