using UnityEngine;

namespace UI
{
    public class CurrentRoomScreen : MonoBehaviour
    {
        [SerializeField] private PlayerListingsMenu playerListingsMenu;
        [SerializeField] private LeaveRoomMenu leaveRoomMenu;
    
        private RoomScreens roomScreens;
        public void FirstInitialize(RoomScreens screens)
        {
            roomScreens = screens;
            leaveRoomMenu.FirstInitialize(screens);
            playerListingsMenu.FirstInitialize(screens);
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}
