using Managers;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UI.Console;
using UnityEngine;
using UnityEngine.UI;
using Hashtable = ExitGames.Client.Photon.Hashtable;

namespace Networking
{
    public class PlayerAttributes : MonoBehaviourPunCallbacks
    {
        [Header("Text Elements")]
        [SerializeField] private TextMeshProUGUI nameFieldHead;
        [SerializeField] private TextMeshProUGUI nameFieldHand;
        [SerializeField] private Image colorFieldHand;
        [SerializeField] private TextMeshProUGUI stateFieldHand;
        [SerializeField] private TextMeshProUGUI imposterFieldHand;

        [Header("Player Values")] 
        [SerializeField] private string myName;
        [SerializeField] private bool isImposter;
        [SerializeField] private Color myColor;
        [SerializeField] private bool isBot;
        public bool isDead;
    
        [Header("Player Parts")] 
        [SerializeField] private MeshRenderer mRenderer;
        [SerializeField] private PhotonView myPhotonView;
        [SerializeField] private Player player;
    
        public PhotonView MyPhotonView => myPhotonView;
        public Player Player => player;
        public string MyName => myName;
        public Color MyColor => myColor;
        public bool IsImposter => isImposter;

        private void Start()
        {
            myPhotonView = GetComponent<PhotonView>();
            if(myPhotonView.IsMine) LoadAttributes();
        }
    
        public void SetPlayerInfo(Player _player, string _name)
        {
            stateFieldHand.text = isDead ? "Dead" : "Alive";
            stateFieldHand.color = isDead ? Color.red : Color.white;
        
            imposterFieldHand.text = isImposter ? "Imposter" : "Regular";
            imposterFieldHand.color = isImposter ? Color.red : Color.green;
        
            player = !isBot ? _player : MyPhotonView.Owner;

            nameFieldHead.text = _name;
            nameFieldHand.text = _name;

            mRenderer.material.color = myColor;
            colorFieldHand.color = myColor;
        }
    
        [PunRPC]
        public void LoadAttributes()
        {
            isDead = false;
            Helper.SetCustomProperty(MyPhotonView,"IsDead",false);
            myName = !isBot ? PhotonNetwork.LocalPlayer.NickName : "Bot";
            myColor = MasterManager.Instance.GameSettings.MyColor;
            Helper.SetCustomProperty(MyPhotonView,"IsImposter",isImposter);        
            SetPlayerInfo(MyPhotonView.Owner, MyName);
        }

        public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
        {
            base.OnPlayerPropertiesUpdate(targetPlayer, changedProps);
            if (targetPlayer == null || !Equals(targetPlayer, player)) return;
            if (changedProps.ContainsKey("IsDead"))
            {
                var newIsDead = Helper.GetCustomProperty(MyPhotonView,"IsDead",false);;
                if(newIsDead != isDead) DeveloperConsole.Instance.AddLine(targetPlayer.NickName + " changed " + changedProps);
                isDead = newIsDead;
                stateFieldHand.text = isDead ? "Dead" : "Alive";
                stateFieldHand.color = isDead ? Color.red : Color.white;
            }
            if (changedProps.ContainsKey("IsImposter"))
            {
                isImposter = Helper.GetCustomProperty(MyPhotonView,"IsImposter",false);;
                imposterFieldHand.text = isImposter ? "Imposter" : "Regular";
                imposterFieldHand.color = isImposter ? Color.red : Color.green;
            }
        }
    }
}
