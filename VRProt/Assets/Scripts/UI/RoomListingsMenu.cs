using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using Utility;

namespace UI
{
    public class RoomListingsMenu : MonoBehaviourPunCallbacks
    {
        [SerializeField] private RoomListing roomListing;
        [SerializeField] private Transform content;
        [SerializeField] private float updateTime = 2f;

        private List<RoomListing> listings = new List<RoomListing>();
        private float currentTime;

        private RoomScreens roomScreens;

        private void Update()
        {		
            //Periodically update the current room listings by rejoining the lobby, since the player count doesnt automatically update.
            //There might be a better way to do this.
            currentTime += Time.deltaTime;
            if (!(currentTime >= updateTime)) return;
            currentTime = 0;
            if (!PhotonNetwork.InLobby) return;
            PhotonNetwork.JoinLobby();
        }
        
        public void FirstInitialize(RoomScreens screens)
        {
            roomScreens = screens;
        }

        //Show the current room on joining and clear/destroy the room listings
        public override void OnJoinedRoom()
        {
            roomScreens.CurrentRoom.Show();
            content.DestroyChildren();
            listings.Clear();
        }

        //Update the room listings when a new one is created (doesn't update existing player count)
        public override void OnRoomListUpdate(List<RoomInfo> roomList)
        {
            foreach (var info in roomList)
            {
                //Removed from Rooms List
                if (info.RemovedFromList)
                {
                    var index = listings.FindIndex(x => x.RoomInfo.Name == info.Name);
                    if (index == -1) continue;
                    Destroy(listings[index].gameObject);
                    listings.RemoveAt(index);
                }
                //Added to Rooms List
                else
                {
                    var index = listings.FindIndex(x => x.RoomInfo.Name == info.Name);
                    if (index == -1)
                    {
                        var listing = Instantiate(roomListing, content);
                        if (listing == null) continue;
                        listing.SetRoomInfo(info);
                        if (listing.roomFull)
                        {
                            Destroy(listing);
                        }
                        else
                        {
                            listings.Add(listing);
                        }
                    }
                    else
                    {
                        listings[index].SetRoomInfo(info);
                    }
                }
            }
        }
    }
}
