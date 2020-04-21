using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.IO;

public class Room : MonoBehaviourPunCallbacks, IInRoomCallbacks
{
    public static Room room;
    PhotonView PV;

    public List<Transform> positions;

    //public bool isLoaded;
    public int currentScene;

    void Awake()
    {
        if (Room.room == null)
            Room.room = this;
        else
        {
            if (Room.room != this)
            {
                Destroy(Room.room.gameObject);
                Room.room = this;
            }
        }
        DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
        PV = GetComponent<PhotonView>();
    }

    public override void OnEnable()
    {
        base.OnEnable();
        PhotonNetwork.AddCallbackTarget(this);
        SceneManager.sceneLoaded += OnSceneFinishedLoading;
    }

    private void OnSceneFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        currentScene = scene.buildIndex;
        if (currentScene == 1)
        {
            CreatePlayer();
        }
    }

    private void CreatePlayer()
    {
        if (PhotonNetwork.CountOfPlayers == 1)
            PhotonNetwork.Instantiate(Path.Combine("Prefab", "Tank"), positions[1].position, positions[1].rotation, 0);
        else
            PhotonNetwork.Instantiate(Path.Combine("Prefab", "Tank"), positions[0].position, positions[0].rotation, 0);
    }

    public override void OnDisable()
    {
        base.OnDisable();
        PhotonNetwork.RemoveCallbackTarget(this);
        SceneManager.sceneLoaded -= OnSceneFinishedLoading;
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        print("We are now in a room");
        if (!PhotonNetwork.IsMasterClient)
            return;
        StartGame();
    }
    void StartGame()
    {
        if (!PhotonNetwork.IsMasterClient)
            return;
        PhotonNetwork.LoadLevel(1);
    }
}
