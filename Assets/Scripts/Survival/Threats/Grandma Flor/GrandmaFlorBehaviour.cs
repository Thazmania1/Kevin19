using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GrandmaFlorBehaviour : MonoBehaviour
{
    // References the interactive water bottle object.
    [SerializeField] private GameObject _interactiveWaterBottle;

    // References the interactive walking stick object.
    [SerializeField] private GameObject _interactiveWalkingStick;

    // References the [Items] folder.
    [SerializeField] private GameObject _items;

    // References the water bottle item and its Image component.
    private Image _waterBottleItemImage;

    // References the walking stick item and its Image component.
    private Image _walkingStickItemImage;

    // References the manager's audio sources.
    private AudioSource _fillWaterSound, _grandmaLaugh;

    // Tracks if the player has the water bottle.
    private bool _hasBottle = false;

    // Tracks if the water bottle is filled.
    private bool _isBottleFilled = true;

    // Tracks if the player holds the walking stick.
    private bool _hasWalkingStick = false;

    // Tracks if grandma still has the walking stick.
    private bool _isWalkingStickFound = true;

    // References the roaming system holder.
    [SerializeField] private GameObject _roamingSystem;
    private RoamingSystem _roamingSystemScript;

    // References the task text object and its TextMeshProUGUI component.
    [SerializeField] private GameObject _taskText;
    private TextMeshProUGUI _taskTextUGUI;

    private void Start()
    {
        AudioSource[] audioSources = gameObject.GetComponents<AudioSource>();
        _fillWaterSound = audioSources[0];
        _grandmaLaugh = audioSources[1];
        Image[] images = _items.GetComponentsInChildren<Image>();
        _waterBottleItemImage = images[1];
        _walkingStickItemImage = images[2];
        _roamingSystemScript = _roamingSystem.GetComponent<RoamingSystem>();
        _taskTextUGUI = _taskText.GetComponent<TextMeshProUGUI>();
        StartCoroutine(GrandmaFlorRandomizer());
    }

    // Consistent method.
    private IEnumerator GrandmaFlorRandomizer()
    {
        // Every 30 seconds, this chick will annoy the player in one of 2 ways.
        while (true)
        {
            _taskTextUGUI.text = "";
            yield return new WaitForSeconds(30);
            int randomTask = Random.Range(0, 2);
            if (randomTask == 0)
            {
                yield return FillWaterBottle();
            }
            else
            {
                yield return FindWalkingStick();
            }
        }
    }

    private IEnumerator FillWaterBottle()
    {
        _isBottleFilled = false;

        // The player has 60 seconds to get the water bottle, fill it, and return it.
        float timeElapsed = 60;
        while (timeElapsed > 0f)
        {
            _taskTextUGUI.text = $"Llena la botella de la abuela y devuelvela: {(int)timeElapsed}s";
            timeElapsed -= Time.deltaTime;
            if (!_hasBottle)
            {
                _interactiveWaterBottle.SetActive(RoamingSystem.CurrentRoom.Name.Equals("Grandma"));
            }
            else if (!_isBottleFilled)
            {
                _isBottleFilled = RoamingSystem.CurrentRoom.Name.Equals("Kitchen");
                if (_isBottleFilled) _fillWaterSound.Play();
            }
            else
            {
                if (RoamingSystem.CurrentRoom.Name.Equals("Grandma"))
                {
                    _hasBottle = false;
                    _grandmaLaugh.Play();
                }
            }

            _waterBottleItemImage.color = !_hasBottle ? new Color32(255, 255, 255, 100) : new Color32(255, 255, 255, 255);
            if (!_hasBottle && _isBottleFilled)
            {
                break;
            }
            yield return null;
        }

        if (!(!_hasBottle && _isBottleFilled))
        {
            StartCoroutine(GetComponent<GrandmaFlorJumpscareHandler>().Jumpscare());
        }
    }

    public void GrabWaterBottle()
    {
        _hasBottle = true;
        _interactiveWaterBottle.SetActive(false);
    }

    private IEnumerator FindWalkingStick()
    {
        Room randomRoom = _roamingSystemScript.Rooms[Random.Range(0, _roamingSystemScript.Rooms.Count - 1)];
        _isWalkingStickFound = false;

        // The player has 90 seconds to get the water bottle, fill it, and return it.
        float timeElapsed = 90;
        while (timeElapsed > 0f)
        {
            _taskTextUGUI.text = $"Busca el bastón de la abuela y traeselo: {(int)timeElapsed}s";
            timeElapsed -= Time.deltaTime;
            if (!_isWalkingStickFound)
            {
                if (!_hasWalkingStick)
                {
                    _interactiveWalkingStick.SetActive(RoamingSystem.CurrentRoom.Name.Equals(randomRoom.Name));
                }
                else
                {
                    if (_hasWalkingStick && !_isWalkingStickFound)
                    {
                        if (RoamingSystem.CurrentRoom.Name.Equals("Grandma"))
                        {
                            _hasWalkingStick = false;
                            _isWalkingStickFound = true;
                            _grandmaLaugh.Play();
                        }
                    }
                }
            }

            _walkingStickItemImage.color = !_hasWalkingStick ? new Color32(255, 255, 255, 100) : new Color32(255, 255, 255, 255);
            if (_isWalkingStickFound && !_hasWalkingStick)
            {
                break;
            }
            yield return null;
        }

        if (!(_isWalkingStickFound && !_hasWalkingStick))
        {
            StartCoroutine(GetComponent<GrandmaFlorJumpscareHandler>().Jumpscare());
        }
    }

    public void GrabWalkingStick()
    {
        _hasWalkingStick = true;
        _interactiveWalkingStick.SetActive(false);
    }
}