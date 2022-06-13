using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Colyseus;
using UnityEngine.UI;

public class GameRoomSelectionMenu : MonoBehaviour
{

    [SerializeField]
    private Button createRoomButton = null;

    [SerializeField]
    private GameObject entryPrefab = null;

    [SerializeField]
    private Transform entryRoot = null;

    [SerializeField]
    private GameObject loadingCover = null;

    [SerializeField]
    private GameLobbyController lobbyController = null;

    private Coroutine refreshRoutine;

    private float refreshTimer = 1.0f;

    [SerializeField]
    private InputField roomCreationNameInputField = null;

    public string RoomCreationName
    {
        get { return roomCreationNameInputField.text; }
    }

    private void Awake()
    {
        createRoomButton.interactable = false;
        loadingCover.SetActive(true);
    }

    private IEnumerator RefreshRoutine()
    {
        while (gameObject.activeSelf)
        {
            yield return new WaitForSeconds(refreshTimer);

            GetAvailableRooms();
        }
    }

    public void OnInputFieldChange()
    {
        createRoomButton.interactable = roomCreationNameInputField.text.Length > 0;
    }

    public void GetAvailableRooms()
    {
        MultiPlayerGameManager.Instance.GetAvailableRooms();
    }

    public void HandRooms(ColyseusRoomAvailable[] rooms)
    {
        loadingCover.SetActive(false);

        if (refreshRoutine == null)
        {
            refreshRoutine = StartCoroutine(RefreshRoutine());
        }

        for (int i = 0; i < entryRoot.childCount; ++i)
        {
            Destroy(entryRoot.GetChild(i).gameObject);
        }

        for (int i = 0; i < rooms.Length; ++i)
        {
            GameObject newEntry = Instantiate(entryPrefab, entryRoot, false);
            GameRoomListItem listItem = newEntry.GetComponent<GameRoomListItem>();
            listItem.Initialize(rooms[i], this);
        }
    }

    public void JoinRoom(string roomID)
    {
        lobbyController.JoinRoom(roomID);
    }
}
