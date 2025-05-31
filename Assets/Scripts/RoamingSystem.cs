using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoamingSystem : MonoBehaviour
{
    // Holds all rooms.
    [SerializeField] private List<Room> _rooms;

    // References the object's Image component.
    private Image _image;

    private void Start()
    {
        _image = GetComponent<Image>();
    }

    // This function is called when clicking a minimap room.
    public void ChangeRoom(string clickedRoomName)
    {
        Debug.Log("I go in MF");
        // Searchs for the corresponding room.
        Room matchingRoom = null;
        foreach (Room room in _rooms)
        {
            if (room.Name.Equals(clickedRoomName))
            {
                Debug.Log("you MF");
                matchingRoom = room;
                break;
            }
            Debug.Log("Not you MF");
        }

        Debug.Log("change MF");
        // Uses the room's sprite.
        _image.sprite = matchingRoom.Image;
    }
}