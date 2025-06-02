using System.Collections;
using UnityEngine;

public class SkibidiToilet : MonoBehaviour
{
    // References the threat's sprites.
    [SerializeField] private Sprite _idle, _flush;

    private void Start()
    {
        StartCoroutine(Timer());
    }

    public IEnumerator Timer()
    {
        yield return null;
    }
}