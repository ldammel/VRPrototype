using UnityEngine;

public class JoinRoomScreen : MonoBehaviour
{
    [SerializeField] private CreateRoom createRoom;
    [SerializeField] private RoomListingsMenu roomListingsMenu;
    
    private RoomScreens roomScreens;
    public void FirstInitialize(RoomScreens screens)
    {
        roomScreens = screens;
        createRoom.FirstInitialize(screens);
        roomListingsMenu.FirstInitialize(screens);
    }
}
