using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Services.Lobbies.Models;
using UnityEngine;

public class LobbyItem : MonoBehaviour{
   [SerializeField]
   private TMP_Text lobbyNameText;
    [SerializeField]
    private TMP_Text lobbyPlayersText;

    private LobbiesList lobbiesList;
    private Lobby lobby;

    public void initialise(LobbiesList list,Lobby lobby) {
        lobbyNameText.text = lobby.Name;
        lobbyPlayersText.text = lobby.Players.Count.ToString() + " / " + lobby.MaxPlayers.ToString();
        this.lobbiesList = list;
        this.lobby = lobby;
    }

    public void join() {
        lobbiesList.joinAsync(lobby);
    }

}
