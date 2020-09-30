using System;
using Photon.Pun;
using Photon.Pun.Demo.Cockpit;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class PlayerAttributes : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI nameField;
    [SerializeField] private MeshRenderer mRenderer;
    [SerializeField] private PhotonView photonView;
    private int colorInt;
    public Player Player;
    public bool isImposter;

    private void Start()
    {
        photonView = GetComponent<PhotonView>();
        if (photonView.IsMine) photonView.RPC("Load", RpcTarget.MasterClient);
    }

    public void StartGames()
    {
        var manager = FindObjectOfType<NetworkManager>();
        manager.StartGame();
    }

    public void SetPlayerInfo(Player player)
    {
        Player = player;
        player.NickName = nameField.text;
        Debug.Log(player.NickName);
        switch (colorInt)
        {
            case 1:
                mRenderer.material.color = Color.blue;   
                break;
            case 2:
                mRenderer.material.color = Color.red;   
                break;
            case 3:
                mRenderer.material.color = Color.green;   
                break;
            case 4:
                mRenderer.material.color = Color.yellow;   
                break;
            default:
                Debug.Log("No Color found!");
                break;
        }
    }


    [PunRPC]
    public void Load()
    {
        photonView.RPC("LoadAttributes", RpcTarget.AllBuffered);
    }

    [PunRPC]
    public void LoadAttributes()
    {
        
        colorInt = SetUpPlayer.Instance.storedColorInt;
        nameField.text = SetUpPlayer.Instance.storedName;
        int state = colorInt;
        string nameState = SetUpPlayer.Instance.storedName;
        Hashtable hash = new Hashtable {{"ColorInt", state}, {"Name", nameState}};
        PhotonNetwork.LocalPlayer.SetCustomProperties(hash);
        SetPlayerInfo(photonView.Owner);
    }
}
