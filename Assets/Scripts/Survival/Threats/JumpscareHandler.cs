using System.Collections;
using UnityEngine;

// Each threat has their subclass of the JumpscareHandler.
public abstract class JumpscareHandler : MonoBehaviour
{
    // Tracks if any threat is jumpscaring.
    protected static bool _isJumpscaring = false;

    // References the [Minimaps] folder.
    [SerializeField] protected GameObject _minimaps;

    // References the [Items] folder.
    [SerializeField] protected GameObject _items;

    // References the [Jumpscares] folder.
    [SerializeField] protected GameObject _jumpscares;

    // References the Task object.
    [SerializeField] protected GameObject _task;

    // References the object's sound source component.
    protected AudioSource _audioSource;

    public abstract IEnumerator Jumpscare();   
}