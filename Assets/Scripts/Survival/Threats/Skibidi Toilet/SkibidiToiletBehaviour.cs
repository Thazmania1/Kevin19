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

    // References the manager's audio source.
    private AudioSource _audioSource;

    // Tracks if the skibidi toilet is flushed.
    private bool _isFlushed = true;

    private void Start()
    {
        _audioSource = gameObject.GetComponent<AudioSource>();
        StartCoroutine(SkibidiRandomizer());
    }

    // Consistent method.
    private IEnumerator SkibidiRandomizer()
    {
        // The game has 1 in 10 chances to spawn a skibidi toilet every 8 seconds.
        while (true)
        {
            yield return new WaitForSeconds(8);
            if (Random.Range(0, 5) == 0) yield return SpawnSkibidiToilet();
        }
    }

    private IEnumerator SpawnSkibidiToilet()
    {
        _isFlushed = false;
        RectTransform randomFloor = _minimaps.transform.GetChild(Random.Range(0, 3)) as RectTransform;
        GameObject spawnedSkibidiToilet = Instantiate(_skibidiToiletCuePrefab, randomFloor);

        // The player has until the song ends to deal with the skibidi toilet.
        _audioSource.Play();
        while (_audioSource.isPlaying)
        {
            yield return null;
        }

        if (!_isFlushed)
        {
            StartCoroutine(GetComponent<SkibidiToiletJumpscareHandler>().Jumpscare());
        }
        else
        {
            _audioSource.Stop();
            Destroy(spawnedSkibidiToilet);
        }
    }
}