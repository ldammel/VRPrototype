using Photon.Pun;
using UnityEngine;

public class LeaveRoomMenu : MonoBehaviour
{
    private RoomScreens roomScreens;

    public void FirstInitialize(RoomScreens screens)
    {
        roomScreens = screens;
    }
    
    public void OnClick_LeaveRoom()
    {
        PhotonNetwork.LeaveRoom(true);
        roomScreens.CurrentRoom.Hide();
    }
}
