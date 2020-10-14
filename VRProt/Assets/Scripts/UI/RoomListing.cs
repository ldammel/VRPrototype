using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;

namespace UI
{
    public class RoomListing : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI roomNameText;
        [SerializeField] private TextMeshProUGUI roomUsersText;
        public bool roomFull;

        public RoomInfo RoomInfo { get; private set; }

        public void SetRoomInfo(RoomInfo roomInfo)
        {
            RoomInfo = roomInfo;
            roomNameText.text = roomInfo.Name;
            roomUsersText.text = roomInfo.PlayerCount + " / " + roomInfo.MaxPlayers;
            roomFull = roomInfo.PlayerCount < roomInfo.MaxPlayers;
        }

        public void OnClick_Button()
        {
            if (RoomInfo.PlayerCount < RoomInfo.MaxPlayers)
            {
                PhotonNetwork.JoinRoom(RoomInfo.Name);
            }
            else
            {
                Debug.Log("Room Full");
            }
        }
    }
}
