using Photon.Realtime;
using TMPro;
using UnityEngine;

namespace UI
{
    public class PlayerListing : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI playerNameText;
        [SerializeField] private Color isLocalColor;

        public Player Player {get; private set;}

        public void SetPlayerInfo(Player newPlayer)
        {
            Player = newPlayer;
            playerNameText.text = Player.NickName;
            if(newPlayer.IsLocal) playerNameText.color = isLocalColor;
        }
    }
}
