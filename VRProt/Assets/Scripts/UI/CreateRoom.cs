using Photon.Pun;
using TMPro;
using UnityEngine;
using Photon.Realtime;

public class CreateRoom : MonoBehaviourPunCallbacks
{
    [SerializeField] private TextMeshProUGUI roomName;
    [SerializeField] private int maxPlayers;
    
    private RoomScreens roomScreens;
    
    public void FirstInitialize(RoomScreens screens)
    {
        roomScreens = screens;
        
    }

    public void OnClick_CreateRoom()
    {
        if (!PhotonNetwork.IsConnected) return;
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = (byte)maxPlayers;
        if (roomName.text == "") roomName.text = "New Room";
        PhotonNetwork.JoinOrCreateRoom(roomName.text, roomOptions, TypedLobby.Default);
    }

    public override void OnCreatedRoom()
    {
       Debug.Log("Created Room successfully.");
       roomScreens.CurrentRoom.Show();
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Room creation failed: " + message);
    }
    
}

