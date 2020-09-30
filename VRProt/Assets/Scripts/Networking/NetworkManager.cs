using Hashtable = ExitGames.Client.Photon.Hashtable;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    public PhotonView view;
    // Start is called before the first frame update
    void Start()
    {
        ConnectToServer();
    }

    void ConnectToServer()
    {
        PhotonNetwork.ConnectUsingSettings();
        Debug.Log("Try Connect To Server...");
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected To Server.");
        base.OnConnectedToMaster();
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 10;
        roomOptions.IsVisible = true;
        roomOptions.IsOpen = true;

        PhotonNetwork.JoinOrCreateRoom("Room 1", roomOptions, TypedLobby.Default);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Joined a Room");
        base.OnJoinedRoom();
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log("A new player joined the room");
        base.OnPlayerEnteredRoom(newPlayer);
    }

    public void StartGame()
    {
        view.RPC("SetMemberStatus", RpcTarget.MasterClient);
    }

    [PunRPC]
    public void SetMemberStatus()
    {
        var currentPlayers = PhotonNetwork.PlayerList;
        Debug.Log(currentPlayers);
        var rNumber = Random.Range(0, currentPlayers.Length - 1);
        if (currentPlayers.Length != 0)
        {
            for (int i = 0; i < currentPlayers.Length-1; i++)
            {
                bool imposter = (bool) currentPlayers[i].CustomProperties["Imposter"];
                bool state = i == rNumber;
                Hashtable hash = new Hashtable();
                hash.Add("Imposter", state);
                currentPlayers[i].SetCustomProperties(hash);
            }
        }
        Debug.Log("Imposter Player No. " + rNumber);
    }
}
