using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SkibidiToiletJumpscareHandler : JumpscareHandler
{
    // References the skibidi jumpscare prefab.
    [SerializeField] private GameObject _skibidiJumpscarePrefab;

    private void Start()
    {
        _audioSource = _jumpscares.GetComponent<AudioSource>();
    }

    public override IEnumerator Jumpscare()
    {
        _isJumpscaring = true; // Makes sure no one else can jumpscare.
        _minimaps.SetActive(false);
        _items.SetActive(false);
        _task.SetActive(false);
        _timeLeft.SetActive(false);
        Instantiate(_skibidiJumpscarePrefab, _jumpscares.transform);
        _audioSource.Play();

        // 1s delay before getting sent to the menu.
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("Menu");
    }
}