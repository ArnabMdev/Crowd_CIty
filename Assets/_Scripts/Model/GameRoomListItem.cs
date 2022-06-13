using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Colyseus;

public class GameRoomListItem : MonoBehaviour
{
    [SerializeField]
    private Text clientCount = null;

    [SerializeField]
    private Button joinButton = null;

    private GameRoomSelectionMenu menuRef;

    [SerializeField]
    private Text roomName = null;

    private ColyseusRoomAvailable roomRef;

    public void Initialize(ColyseusRoomAvailable roomReference, GameRoomSelectionMenu menu)
    {
        menuRef = menu;
        roomRef = roomReference;
        roomName.text = roomReference.roomId;
        string maxClients = roomReference.maxClients > 0 ? roomReference.maxClients.ToString() : "--";
        clientCount.text = $"{roomReference.clients} / {maxClients}";
        //TODO: if we want to lock rooms, will need to do so here
        if (roomReference.maxClients > 0 && roomReference.clients >= roomReference.maxClients)
        {
            joinButton.interactable = false;
        }
        else
        {
            joinButton.interactable = true;
        }
    }

    public void TryJoin()
    {
        menuRef.JoinRoom(roomRef.roomId);
    }
}
