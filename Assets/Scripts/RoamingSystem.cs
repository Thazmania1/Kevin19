using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoamingSystem : MonoBehaviour
{
    // Holds all rooms.
    [SerializeField] private List<Room> _rooms;

    // Tracks the current room.
    private Room _currentRoom;

    // References the object's Image component.
    private Image _image;

    // References the minimap floors.
    [SerializeField] private GameObject[] _floors;

    private void Start()
    {
        _image = GetComponent<Image>();

        // Sets Kevin's room as the starting room.
        foreach (Room room in _rooms)
        {
            if (room.Name.Equals("Kevin"))
            {
                _currentRoom = room;
                break;
            }
        }
        UpdateAccessibleRooms(_currentRoom);
    }

    // This function is called when clicking a minimap room.
    public void ChangeRoom(string clickedRoomName)
    {
        // Searchs for the corresponding room.
        foreach (Room room in _rooms)
        {
            if (room.Name.Equals(clickedRoomName))
            {
                _currentRoom = room;
                break;
            }
        }

        // Uses the room's sprite.
        _image.sprite = _currentRoom.Image;

        // Updates the accesible rooms.
        UpdateAccessibleRooms(_currentRoom);
    }

    public void UpdateAccessibleRooms(Room room)
    {
        // Searches through every floor and their rooms.
        foreach (GameObject floor in _floors)
        {
            foreach (Transform roomObject in floor.transform)
            {
                // Next object if it's the the floor's text.
                if (roomObject.name.EndsWith("Text")) continue;

                // Handles the logic of room navigation, alongside with visual cues.
                Transform roomObjectClickable = roomObject.GetChild(0);
                Image roomObjectClickableImage = roomObjectClickable.GetComponent<Image>();
                Button roomObjectClickableButton = roomObjectClickable.GetComponent<Button>();
                if (room.AccessibleRooms.Contains(roomObject.name))
                {
                    roomObjectClickableImage.color = Color.yellow;
                    roomObjectClickableButton.enabled = true;
                }
                else
                {
                    if (!roomObject.name.Equals(_currentRoom.Name))
                    {
                        roomObjectClickableImage.color = Color.gray;
                    }
                    else
                    {
                        roomObjectClickableImage.color = Color.red;
                    }
                    roomObjectClickableButton.enabled = false;
                }
            }
        }
    }
}