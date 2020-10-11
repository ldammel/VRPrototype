using System.Collections;
using System.Linq;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using Random = UnityEngine.Random;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private PhotonView mainView;
    [SerializeField] private TextMeshProUGUI playerCounter;
    private Player[] currentPlayers;
    private int playersInRoom;
    public bool isImposterInitialized;
    
    private void Start()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        ConnectToServer();
    }

    #region Connecting
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
    #endregion

    #region Joining
    public override void OnJoinedLobby()
    {
        Debug.Log(PhotonNetwork.LocalPlayer.NickName + " Joined the Lobby");
        base.OnJoinedLobby();
    }

    public override void OnJoinedRoom()
    {
        Debug.Log(PhotonNetwork.LocalPlayer.NickName + " Joined a Room");
        base.OnJoinedRoom();
        currentPlayers = PhotonNetwork.PlayerList;
        playersInRoom = currentPlayers.Length;
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log(newPlayer.NickName +" joined the room");
        base.OnPlayerEnteredRoom(newPlayer);
        currentPlayers = PhotonNetwork.PlayerList;
        playersInRoom++;
        playerCounter.text = PhotonNetwork.CurrentRoom.PlayerCount + " / " + PhotonNetwork.CurrentRoom.MaxPlayers;
        if (playersInRoom == PhotonNetwork.CurrentRoom.MaxPlayers)
        {
            PhotonNetwork.CurrentRoom.IsOpen = false;
        }
    }
    #endregion

    #region Disconnect
    public void Disconnect()
    {
        StartCoroutine(DisconnectAndLoad());
    }

    private IEnumerator DisconnectAndLoad()
    {
        PhotonNetwork.Disconnect();
        while (PhotonNetwork.IsConnected) yield return null;
        PhotonNetwork.LoadLevel(0);
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        base.OnPlayerLeftRoom(otherPlayer);
        DeveloperConsole.Instance.AddLine(otherPlayer.NickName + " left the game.");
        playersInRoom--;
    }
    #endregion
    
    public void GameStart()
    {
        var maxImposterAmount = (int)PhotonNetwork.CurrentRoom.CustomProperties["MaxImposter"];
        if (PhotonNetwork.PlayerList == null)
        {
            Debug.LogError("No Players Found!");
            DeveloperConsole.Instance.AddLine("No Players Found!");
            return;
        }
        var activePlayers = PhotonNetwork.PlayerList.ToList();
        DeveloperConsole.Instance.AddLine(activePlayers.ToString());
        if(maxImposterAmount > activePlayers.Count) Debug.LogError("To Many Imposters!");
        for (int i = 0; i < maxImposterAmount; i++)
        {
            int index = Random.Range(0, activePlayers.Count);
            if (activePlayers[index] == null) return;
            Hashtable prop = new Hashtable();
            prop.Add("IsImposter",true);
            activePlayers[index].SetCustomProperties(prop);
            activePlayers.RemoveAt(index);
        }
        isImposterInitialized = true;
        mainView.RPC("LoadAttributes", RpcTarget.AllBuffered);
    }
}
