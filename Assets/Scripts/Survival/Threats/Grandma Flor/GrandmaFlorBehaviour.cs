using System.Collections;
using UnityEngine;

public class GrandmaFlorBehaviour : MonoBehaviour
{
    // References the interactive water bottle object.
    [SerializeField] private GameObject _interactiveWaterBottle;

    // References the [Items] folder.
    [SerializeField] private GameObject _items;

    // References the manager's audio source.
    private AudioSource _fillWaterSound;

    // Tracks if the player has the water bottle.
    private bool _hasBottle = false;

    // Tracks if the water bottle is filled.
    private bool _isBottleFilled = true;

    private void Start()
    {
        _fillWaterSound = gameObject.GetComponent<AudioSource>();
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
        // The player has 60 seconds to get the water bottle, fill it, and return it.
        float timeElapsed = 0;
        while (timeElapsed < 60f)
        {

            yield return null;
        }
    }
}