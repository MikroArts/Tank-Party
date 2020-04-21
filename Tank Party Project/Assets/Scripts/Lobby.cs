using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lobby : MonoBehaviourPunCallbacks
{
    public static Lobby lobby;

    public GameObject battleButton;
    public GameObject cancelButton;

    void Avake()
    {
        lobby = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        print("Player has connected to the Photon mAster server");
        PhotonNetwork.AutomaticallySyncScene = true;
        battleButton.SetActive(true);
    }

    public void OnClick_BattleButton()
    {
        battleButton.SetActive(false);
        cancelButton.SetActive(true);
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        print("Tried to join a random game room failed.");
        CreateRoom();

    }

    private static void CreateRoom()
    {
        int randomRoomName = Random.Range(0, 10000);
        RoomOptions options = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = 4 };
        PhotonNetwork.CreateRoom("Room" + randomRoomName, options);
    }
    

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        print("Tried to create room failed");
        CreateRoom();
    }

    public void OnClick_CancelButton()
    {
        cancelButton.SetActive(false);
        battleButton.SetActive(true);
        PhotonNetwork.LeaveRoom();
    }
}
