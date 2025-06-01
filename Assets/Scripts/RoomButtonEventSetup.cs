using UnityEngine;
using UnityEngine.UI;

// Dynamically sets up the roaming system.
public class RoomButtonEventSetup : MonoBehaviour
{
    private void Start()
    {
        GameObject roamingSystemHolder = GameObject.Find("CurrentRoom");
        RoamingSystem roamingSystemReference = roamingSystemHolder.GetComponent<RoamingSystem>();
        Button buttonComponent = gameObject.GetComponent<Button>();
        buttonComponent.onClick.AddListener(() => roamingSystemReference.ChangeRoom(transform.parent.name));
    }
}