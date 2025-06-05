using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GrandmaFlorBehaviour : MonoBehaviour
{
    // References the interactive water bottle object.
    [SerializeField] private GameObject _interactiveWaterBottle;

    // References the water bottle item and its Image component.
    [SerializeField] private GameObject _waterBottleItem;
    private Image _waterBottleItemImage;

    // References the [Items] folder.
    [SerializeField] private GameObject _items;

    // References the manager's audio sources.
    private AudioSource _fillWaterSound, _grandmaLaugh;

    // Tracks if the player has the water bottle.
    private bool _hasBottle = false;

    // Tracks if the water bottle is filled.
    private bool _isBottleFilled = true;

    private void Start()
    {
        AudioSource[] audioSources = gameObject.GetComponents<AudioSource>();
        _fillWaterSound = audioSources[0];
        _grandmaLaugh = audioSources[1];
        _waterBottleItemImage = _waterBottleItem.GetComponent<Image>();
        StartCoroutine(GrandmaFlorRandomizer());
    }

    // Consistent method.
    private IEnumerator GrandmaFlorRandomizer()
    {
        // Every 30 secons, this chick will annoy the player in one of 2 ways.
        while (true)
        {
            yield return new WaitForSeconds(30);
            int randomTask = Random.Range(0, 2);
            if (randomTask == 0)
            {
                yield return FillWaterBottle();
            }
            else
            {

            }
        }
    }

    private IEnumerator FillWaterBottle()
    {
        _isBottleFilled = false;

        // The player has 60 seconds to get the water bottle, fill it, and return it.
        float timeElapsed = 0;
        while (timeElapsed < 60f)
        {
            timeElapsed += Time.deltaTime;
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
            // TODO: Jumpscare.
        }
    }

    public void GrabWaterBottle()
    {
        _hasBottle = true;
        _interactiveWaterBottle.SetActive(false);
    }
}