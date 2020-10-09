using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    private void Start()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        ConnectToServer();
    }

    private void ConnectToServer()
    {
        PhotonNetwork.GameVersion = MasterManager.Instance.GameSettings.GameVersion;
        PhotonNetwork.NickName = MasterManager.Instance.GameSettings.NickName;
        PhotonNetwork.ConnectUsingSettings();
        Debug.Log("Try Connect To Server...");
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log(PhotonNetwork.LocalPlayer.NickName +" has Connected To Server.");
        if(!PhotonNetwork.InLobby)PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        Debug.Log(PhotonNetwork.LocalPlayer.NickName + " Joined the Lobby");
        base.OnJoinedLobby();
    }

    public override void OnJoinedRoom()
    {
        Debug.Log(PhotonNetwork.LocalPlayer.NickName + " Joined a Room");
        base.OnJoinedRoom();
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log(newPlayer.NickName +" joined the room");
        base.OnPlayerEnteredRoom(newPlayer);
    }
    
    [PunRPC]
    public void UpdatePlayer(PhotonView view)
    {
        DeveloperConsole.Instance.AddLine("Set " + view.gameObject.name + " status to Dead");
        Helper.SetCustomProperty(view,"IsDead",true,true);
    }
}
