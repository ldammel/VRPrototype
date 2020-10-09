using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class PlayerAttributes : MonoBehaviourPunCallbacks , IPunObservable
{
    [SerializeField] private string myName;
    [SerializeField] private TextMeshProUGUI nameFieldHead;
    [SerializeField] private TextMeshProUGUI nameFieldHand;
    [SerializeField] private TextMeshProUGUI colorFieldHand;
    [SerializeField] private TextMeshProUGUI stateFieldHand;
    [SerializeField] private MeshRenderer mRenderer;
    
    public PhotonView myPhotonView;
    public Player player;
    public bool isImposter;
    public bool isBot;
    
    private int colorInt;
    public bool isDead;

    private void Start()
    {
        myPhotonView = GetComponent<PhotonView>();
        if (myPhotonView.IsMine) myPhotonView.RPC("LoadAttributes", RpcTarget.AllBuffered);;
    }
    
    public void SetPlayerInfo(Player player, int _colorInt, string _name)
    {
        stateFieldHand.text = isDead ? "Dead" : "Alive";
        if (!isBot)
        {
            this.player = player;
            player.NickName = _name;
        }

        nameFieldHead.text = _name;
        nameFieldHand.text = _name;
        switch (_colorInt)
        {
            case 1:
                mRenderer.material.color = Color.blue;
                colorFieldHand.text = "Color: Blue";
                break;
            case 2:
                mRenderer.material.color = Color.red;   
                colorFieldHand.text = "Color: Red";
                break;
            case 3:
                mRenderer.material.color = Color.green;   
                colorFieldHand.text = "Color: Green";
                break;
            case 4:
                mRenderer.material.color = Color.yellow;   
                colorFieldHand.text = "Color: Yellow";
                break;
            default:
                Debug.Log("No Color found!");
                break;
        }
    }

    [PunRPC]
    public void LoadAttributes()
    {
        Helper.SetCustomProperty(myPhotonView,"IsDead",false,false);
        
        if(!isBot)myName = MasterManager.Instance.GameSettings.NickName;
        colorInt = MasterManager.Instance.GameSettings.ColorInt;
        isDead = MasterManager.Instance.GameSettings.IsDead;
        
        SetPlayerInfo(myPhotonView.Owner, colorInt, myName);
        stateFieldHand.text = isDead ? "Dead" : "Alive";
    }

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
    {
        base.OnPlayerPropertiesUpdate(targetPlayer, changedProps);
        if (targetPlayer != null && targetPlayer == player)
        {
            Debug.Log(targetPlayer.NickName + " changed " + changedProps);
            if (changedProps.ContainsKey("IsDead"))
            {
                isDead = Helper.GetCustomProperty(myPhotonView,"IsDead",false,false);;
                stateFieldHand.text = isDead ? "Dead" : "Alive";
            }
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(this.isDead);
            stream.SendNext(this.colorInt);
        }
        else
        {
            this.isDead = (bool) stream.ReceiveNext();
            this.colorInt = (int) stream.ReceiveNext();
        }
    }
}
