using System.Collections;
using UnityEngine;

public class SkibidiToiletBehaviour : MonoBehaviour
{
    // References the skibidi toilet cue prefab.
    [SerializeField] private GameObject _skibidiToiletCuePrefab;

    // References the skibidi toilet's sprites.
    [SerializeField] private Sprite _idle, _flush;

    // References the [Minimaps] folder.
    [SerializeField] private GameObject _minimaps;

    // References the [Interactive] folder.
    [SerializeField] private GameObject _interactive;

    // References the manager's audio sources.
    private AudioSource _skibidiSong, _flushSound;

    // References the interactive skibidi toilet object.
    [SerializeField] private GameObject _interactiveSkibidiToilet;

    // Tracks if the interactive skibidi toilet is flushed.
    private bool _isFlushed = true;

    // References the current floor bathroom the interactive skibidi toilet is in.
    private string _currentBathroom = "";

    private void Start()
    {
        AudioSource[] audioSources = gameObject.GetComponents<AudioSource>();
        _skibidiSong = audioSources[0];
        _flushSound = audioSources[1];
        StartCoroutine(SkibidiRandomizer());
    }

    // Consistent method.
    private IEnumerator SkibidiRandomizer()
    {
        // The game has 1 in 5 chances to spawn a skibidi toilet every 8 seconds.
        while (true)
        {
            yield return new WaitForSeconds(8);
            if (Random.Range(0, 5) == 0) yield return SpawnSkibidiToilet();
        }
    }

    private IEnumerator SpawnSkibidiToilet()
    {
        _isFlushed = false;
        RectTransform randomFloor = _minimaps.transform.GetChild(Random.Range(0, _minimaps.transform.childCount)) as RectTransform;
        GameObject spawnedSkibidiToilet = Instantiate(_skibidiToiletCuePrefab, randomFloor);
        _currentBathroom = $"{randomFloor.name.Substring(1, randomFloor.name.Length - 2)}Bathroom";

        // The player has until the song ends to deal with the skibidi toilet.
        _skibidiSong.Play();
        while (_skibidiSong.isPlaying && !_isFlushed)
        {
            _interactiveSkibidiToilet.SetActive(RoamingSystem.CurrentRoom.Name.Equals(_currentBathroom)); // Notifies the script of when the player is currently in the same bathroom as skibidi toilet.
            yield return null;
        }
        if (!_isFlushed)
        {
            StartCoroutine(GetComponent<SkibidiToiletJumpscareHandler>().Jumpscare());
        }
        else
        {
            _skibidiSong.Stop();
            Destroy(spawnedSkibidiToilet);
            _interactiveSkibidiToilet.SetActive(false);
            _currentBathroom = "";
            _flushSound.Play();
        }
    }

    // Called when clicking an interactable skibidi toilet.
    public void FlushSkibidiToilet()
    {
        _isFlushed = true;
    }
}