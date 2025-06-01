using System.Collections.Generic;
using UnityEngine;

// Defines the room itself, and what rooms can be accessed from within.
[System.Serializable] public class Room
{
    [SerializeField] private string _name;
    [SerializeField] private Sprite _image;
    [SerializeField] private List<string> _accessibleRooms;

    // Getters.
    public string Name => _name;
    public Sprite Image => _image;
    public List<string> AccessibleRooms => _accessibleRooms;
}