using UnityEngine;

namespace UI
{
    public class RoomScreens : MonoBehaviour
    {
        [SerializeField] private CurrentRoomScreen currentRoom;
        public CurrentRoomScreen CurrentRoom => currentRoom;

        [SerializeField] private JoinRoomScreen joinRoomScreen;
        public JoinRoomScreen JoinRoomScreen => joinRoomScreen;

        private void Awake()
        {
            FirstInitialize();
        }

        private void FirstInitialize()
        {
            CurrentRoom.FirstInitialize(this);
            JoinRoomScreen.FirstInitialize(this);
        }
    }
}
