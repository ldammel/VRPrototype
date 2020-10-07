using System;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

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
        currentTime += Time.deltaTime;
        if (currentTime >= updateTime)
        {
            currentTime = 0;
            if (!PhotonNetwork.InLobby) return;
            PhotonNetwork.JoinLobby();
        }
    }

    public void FirstInitialize(RoomScreens screens)
    {
        roomScreens = screens;
    }

    public override void OnJoinedRoom()
    {
        roomScreens.CurrentRoom.Show();
        content.DestroyChildren();
        listings.Clear();
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach (var info in roomList)
        {
            //Removed from Rooms List
            if (info.RemovedFromList)
            {
                var index = listings.FindIndex(x => x.RoomInfo.Name == info.Name);
                if (index != -1)
                {
                    Destroy(listings[index].gameObject);
                    listings.RemoveAt(index);
                }
            }
            //Added to Rooms List
            else
            {
                int index = listings.FindIndex(x => x.RoomInfo.Name == info.Name);
                if (index == -1)
                {
                    var listing = Instantiate(roomListing, content);
                    if (listing == null) continue;
                    listing.SetRoomInfo(info);
                    listings.Add(listing);
                }
                else
                {
                    listings[index].SetRoomInfo(info);
                }
            }
        }
    }
}
