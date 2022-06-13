using System;
using System.Collections;
using System.Collections.Generic;
using Colyseus;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLobbyController : MonoBehaviour
{

    [SerializeField]
    private GameObject connectingCover = null;

    [SerializeField]
    private GameCreateUserMenu createUserMenu = null;

    public int minRequiredPlayers = 2;

    //Variables to initialize the room controller
    public string roomName = "my_room";
    public string gameSceneName = "ExampleScene";
    [SerializeField]
    private GameRoomSelectionMenu selectRoomMenu = null;

    private void Awake()
    {
        createUserMenu.gameObject.SetActive(true);
        selectRoomMenu.gameObject.SetActive(false);
        connectingCover.SetActive(true);
    }

    private IEnumerator Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        while (!MultiPlayerGameManager.IsReady)
        {
            yield return new WaitForEndOfFrame();
        }

        Dictionary<string, object> roomOptions = new Dictionary<string, object>
        {
            ["YOUR_ROOM_OPTION_1"] = "option 1",
            ["YOUR_ROOM_OPTION_2"] = "option 2"
        };

        MultiPlayerGameManager.Instance.Initialize(roomName, roomOptions);
        MultiPlayerGameManager.onRoomsReceived += OnRoomsReceived;
        connectingCover.SetActive(false);
    }

    private void OnDestroy()
    {
        MultiPlayerGameManager.onRoomsReceived -= OnRoomsReceived;
    }

    public void CreateUser()
    {
        string desiredUserName = createUserMenu.UserName;
        PlayerPrefs.SetString("UserName", desiredUserName);

        ColyseusSettings clonedSettings = MultiPlayerGameManager.Instance.CloneSettings();
        clonedSettings.colyseusServerAddress = createUserMenu.ServerURL;
        clonedSettings.colyseusServerPort = createUserMenu.ServerPort;
        clonedSettings.useSecureProtocol = createUserMenu.UseSecure;

        MultiPlayerGameManager.Instance.OverrideSettings(clonedSettings);

        MultiPlayerGameManager.Instance.InitializeClient();

        MultiPlayerGameManager.Instance.UserName = desiredUserName;

        //Do user creation stuff
        createUserMenu.gameObject.SetActive(false);
        selectRoomMenu.gameObject.SetActive(true);
        selectRoomMenu.GetAvailableRooms();
    }

    public void CreateRoom()
    {
        connectingCover.SetActive(true);
        string desiredRoomName = selectRoomMenu.RoomCreationName;
        if (!string.IsNullOrEmpty(desiredRoomName))
        {
            LoadGallery(() => { MultiPlayerGameManager.Instance.CreateNewRoom(desiredRoomName); });
        }
    }

    public void JoinOrCreateRoom()
    {
        connectingCover.SetActive(true);
        LoadGallery(() => { MultiPlayerGameManager.Instance.JoinOrCreateRoom(); });
    }

    public void JoinRoom(string id)
    {
        connectingCover.SetActive(true);
        LoadGallery(() => { MultiPlayerGameManager.Instance.JoinExistingRoom(id); });
    }

    public void OnConnectedToServer()
    {
        connectingCover.SetActive(false);
    }

    private void OnRoomsReceived(ColyseusRoomAvailable[] rooms)
    {
        selectRoomMenu.HandRooms(rooms);
    }

    private void LoadGallery(Action onComplete)
    {
        StartCoroutine(LoadSceneAsync(gameSceneName, onComplete));
    }

    private IEnumerator LoadSceneAsync(string scene, Action onComplete)
    {
        Scene currScene = SceneManager.GetActiveScene();
        AsyncOperation op = SceneManager.LoadSceneAsync(scene, LoadSceneMode.Additive);
        while (op.progress <= 0.9f)
        {
            //Wait until the scene is loaded
            yield return new WaitForEndOfFrame();
        }

        onComplete.Invoke();
        op.allowSceneActivation = true;
        SceneManager.UnloadSceneAsync(currScene);
    }

}
