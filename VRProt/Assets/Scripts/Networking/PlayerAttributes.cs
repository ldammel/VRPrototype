using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class PlayerAttributes : MonoBehaviourPunCallbacks , IPunObservable
{
    [SerializeField] private string myName;
    [SerializeField] private TextMeshProUGUI nameField;
    [SerializeField] private MeshRenderer mRenderer;
    
    public PhotonView myPhotonView;
    public Player player;
    public bool isImposter;
    
    private int colorInt;
    private bool isDead;

    private void Start()
    {
        myPhotonView = GetComponent<PhotonView>();
        if (myPhotonView.IsMine) myPhotonView.RPC("LoadAttributes", RpcTarget.AllBuffered);;
    }
    
    public void SetPlayerInfo(Player player, int _colorInt, string _name)
    {
        this.player = player;
        player.NickName = _name;
        nameField.text = _name;
        switch (_colorInt)
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
    public void LoadAttributes()
    {
        Helper.SetCustomProperty(myPhotonView,"ColorInt",SetUpPlayer.Instance.storedColorInt,SetUpPlayer.Instance.storedColorInt);
        Helper.SetCustomProperty(myPhotonView,"Name",SetUpPlayer.Instance.storedName,SetUpPlayer.Instance.storedName);
        Helper.SetCustomProperty(myPhotonView,"IsDead",false,false);
        
        colorInt = Helper.GetCustomProperty(myPhotonView,"ColorInt",SetUpPlayer.Instance.storedColorInt,SetUpPlayer.Instance.storedColorInt);
        myName = Helper.GetCustomProperty(myPhotonView,"Name",SetUpPlayer.Instance.storedName,SetUpPlayer.Instance.storedName);
        
        SetPlayerInfo(myPhotonView.Owner, colorInt, myName);
    }

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
    {
        base.OnPlayerPropertiesUpdate(targetPlayer, changedProps);
        if (targetPlayer != null && targetPlayer == player)
        {
            Debug.Log(targetPlayer.NickName + " changed " + changedProps);
            if (changedProps.ContainsKey("IsDead"))
            {
                isDead = changedProps.TryGetValue("IsDead",out var value);
            }
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(this.isDead);
            stream.SendNext(this.colorInt);
            stream.SendNext(this.myName);
        }
        else
        {
            this.isDead = (bool) stream.ReceiveNext();
            this.colorInt = (int) stream.ReceiveNext();
            this.myName = (string) stream.ReceiveNext();
        }
    }
}
