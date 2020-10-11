using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class PlayerAttributes : MonoBehaviourPunCallbacks
{
    [Header("Text Elements")]
    [SerializeField] private TextMeshProUGUI nameFieldHead;
    [SerializeField] private TextMeshProUGUI nameFieldHand;
    [SerializeField] private TextMeshProUGUI colorFieldHand;
    [SerializeField] private TextMeshProUGUI stateFieldHand;
    [SerializeField] private TextMeshProUGUI imposterFieldHand;

    [Header("Player Values")] 
    [SerializeField] private string myName;
    [SerializeField] private bool isImposter;
    [SerializeField] private int colorInt;
    [SerializeField] private bool isBot;
    public bool isDead;
    
    [Header("Player Parts")] 
    [SerializeField] private MeshRenderer mRenderer;
    [SerializeField] private PhotonView myPhotonView;
    [SerializeField] private Player player;
    
    public PhotonView MyPhotonView => myPhotonView;
    public Player Player => player;
    public string MyName => myName;
    public int ColorInt => colorInt;
    public bool IsImposter => isImposter;

    private void Start()
    {
        myPhotonView = GetComponent<PhotonView>();
        if(myPhotonView.IsMine) LoadAttributes();
    }
    
    public void SetPlayerInfo(Player _player, int _colorInt, string _name)
    {
        stateFieldHand.text = isDead ? "Dead" : "Alive";
        stateFieldHand.color = isDead ? Color.red : Color.white;
        
        imposterFieldHand.text = isImposter ? "Imposter" : "Regular";
        imposterFieldHand.color = isImposter ? Color.red : Color.green;
        
        player = !isBot ? _player : MyPhotonView.Owner;

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
        Helper.SetCustomProperty(MyPhotonView,"IsDead",false,false);
        myName = !isBot ? PhotonNetwork.LocalPlayer.NickName : "Bot";
        colorInt = MasterManager.Instance.GameSettings.ColorInt;
        isDead = false;
        Helper.SetCustomProperty(MyPhotonView,"IsImposter",isImposter,isImposter);        
        SetPlayerInfo(MyPhotonView.Owner, ColorInt, MyName);
    }

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
    {
        base.OnPlayerPropertiesUpdate(targetPlayer, changedProps);
        if (targetPlayer != null && Equals(targetPlayer, player))
        {
            if (changedProps.ContainsKey("IsDead"))
            {
                var newIsDead = Helper.GetCustomProperty(MyPhotonView,"IsDead",false,false);;
                if(newIsDead != isDead) DeveloperConsole.Instance.AddLine(targetPlayer.NickName + " changed " + changedProps);
                isDead = newIsDead;
                stateFieldHand.text = isDead ? "Dead" : "Alive";
                stateFieldHand.color = isDead ? Color.red : Color.white;
            }
            if (changedProps.ContainsKey("IsImposter"))
            {
                isImposter = Helper.GetCustomProperty(MyPhotonView,"IsImposter",false,false);;
                imposterFieldHand.text = isImposter ? "Imposter" : "Regular";
                imposterFieldHand.color = isImposter ? Color.red : Color.green;
            }
        }
    }
}
