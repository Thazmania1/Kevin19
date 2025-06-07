using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;

public class RoamingSystem : MonoBehaviour
{
    // Holds all rooms.
    [SerializeField] private List<Room> _rooms;

    // Tracks the current room.
    private static Room _currentRoom;

    // References the object's Image component.
    private Image _image;

    // References the [Minimaps] folder.
    [SerializeField] private GameObject _minimaps;

    // References the transition cover's Image component.
    [SerializeField] private GameObject _transitionCover;
    private Image _transitionCoverImage;

    // Tracks if the player can change rooms.
    private bool _canChangeRoom = true;

    // References the object's audio source component.
    private AudioSource _audioSource;

    private void Start()
    {
        _transitionCoverImage = _transitionCover.GetComponent<Image>();

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

        _audioSource = GetComponent<AudioSource>();
        UpdateAccessibleRooms(_currentRoom);
    }

    // This function is called when clicking a minimap room.
    public IEnumerator ChangeRoom(string clickedRoomName)
    {
        if (!_canChangeRoom) yield break;
        _canChangeRoom = false;

        _audioSource.Play();
        yield return StartCoroutine(Transition(true));

        // Searchs for the corresponding room.
        foreach (Room room in _rooms)
        {
            if (room.Name.Equals(clickedRoomName))
            {
                _currentRoom = room;
                break;
            }
        }

        _image.sprite = _currentRoom.Image;
        UpdateAccessibleRooms(_currentRoom);
        yield return new WaitForSeconds(0.25f); // A little delay for detail.
        yield return StartCoroutine(Transition(false));
        _canChangeRoom = true;
    }

    public void UpdateAccessibleRooms(Room room)
    {
        // Searches through every floor and their rooms.
        foreach (Transform floor in _minimaps.transform)
        {
            foreach (Transform roomObject in floor)
            {
                // Next object if it's the the floor's text.
                if (roomObject.name.EndsWith("Text")) break;

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

    // This method covers the screen completely in black for loading transitions.
    public IEnumerator Transition(bool isLoading)
    {
        // Forced transition handler.
        Color color = _transitionCoverImage.color;
        float endAlpha = isLoading ? 1f : 0f;
        color = isLoading ? new Color(color.r, color.g, color.b, 0) : new Color(color.r, color.g, color.b, 1);
        float startAlpha = color.a;

        // 0.5s timer.
        float timeElapsed = 0f;
        float timeObjective = 1f;
        while (timeElapsed < timeObjective)
        {
            timeElapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, endAlpha, timeElapsed / timeObjective);
            _transitionCoverImage.color = new Color(color.r, color.g, color.b, alpha);
            yield return null;
        }
        _transitionCoverImage.color = new Color(color.r, color.g, color.b, endAlpha);
    }

    // Getters.
    public static Room CurrentRoom => _currentRoom;
    public List<Room> Rooms => _rooms;
}