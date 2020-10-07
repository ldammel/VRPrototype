﻿using System;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class PlayerListingsMenu : MonoBehaviourPunCallbacks
{
    [SerializeField] private PlayerListing playerListing;
    [SerializeField] private Transform content;

    private List<PlayerListing> listings = new List<PlayerListing>();

    private RoomScreens roomScreens;

    public override void OnEnable()
    {
        base.OnEnable();
        GetCurrentRoomPlayers();
    }

    public override void OnDisable()
    {
        base.OnDisable();
        for (int i = 0; i < listings.Count; i++)
        {
            Destroy(listings[i].gameObject);
        }
        listings.Clear();
    }

    public void FirstInitialize(RoomScreens screens)
    {
        roomScreens = screens;
    }

    private void GetCurrentRoomPlayers()
    {
        if (!PhotonNetwork.IsConnected) return;
        if (PhotonNetwork.CurrentRoom == null || PhotonNetwork.CurrentRoom.Players == null) return;
        
        foreach (var playerInfo in PhotonNetwork.CurrentRoom.Players)
        {
            AddPlayerListing(playerInfo.Value);
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        AddPlayerListing(newPlayer);
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        var index = listings.FindIndex(x => x.Player == otherPlayer);
        if (index != -1)
        {
            Destroy(listings[index].gameObject);
            listings.RemoveAt(index);
        }
    }
    
    private void AddPlayerListing(Player player)
    {
        int index = listings.FindIndex(x => x.Player == player);
        if (index != -1)
        {
            listings[index].SetPlayerInfo(player);
        }
        else
        {
            var listing = Instantiate(playerListing, content);
            if (listing == null) return;
            listing.SetPlayerInfo(player);
            listings.Add(listing);
        }
    }
}
