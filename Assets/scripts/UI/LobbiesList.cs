using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;
using UnityEngine;

public class LobbiesList : MonoBehaviour {
    private bool isJoining = false;
    private Lobby lobbyJoined;
    private bool isRefreshing;
    private int MAX_LOBBY_COUNT = 10;

    [SerializeField]
    private Transform lobbyItemParent;

    [SerializeField]
    private LobbyItem lobbyItemPrefab;


    private void OnEnable() {
        refreshList();
    }

    public async void refreshList() {
        if (isRefreshing) { return; }
        isRefreshing = true;

        QueryResponse response = await queryLobby();

        foreach (Transform child in lobbyItemParent) {
            Destroy(child.gameObject);
        }

        foreach (Lobby lobby in response.Results) {
            LobbyItem item = Instantiate(lobbyItemPrefab, lobbyItemParent);
            item.initialise(this, lobby);
        }


        isRefreshing = false;
    }

    private async Task<QueryResponse> queryLobby() {
        QueryResponse response = null;
        QueryLobbiesOptions options = new QueryLobbiesOptions();
        try {

            options.Count = 25;
            QueryFilter qfSlots = new QueryFilter(
                field: QueryFilter.FieldOptions.AvailableSlots,
                 op: QueryFilter.OpOptions.GT,
                 value: "0"
            );
            QueryFilter qfLocked = new QueryFilter(
               field: QueryFilter.FieldOptions.IsLocked,
                op: QueryFilter.OpOptions.EQ,
                value: "0"
           );

            response = await Lobbies.Instance.QueryLobbiesAsync(options);

            options.Filters = new List<QueryFilter>() { qfSlots, qfLocked };
        } catch (Exception e) {
            throw e;
        }
        return response;

    }





    public async void joinAsync(Lobby lobby) {
        if (isJoining) { return; }
        isJoining = true;
        try {
            lobbyJoined = await Lobbies.Instance.JoinLobbyByIdAsync(lobby.Id);
            string joinCode = lobbyJoined.Data["JoinCode"].Value;

            await ClientSingelton.Instance.gameManager.startClientAsync(joinCode);

        } catch (Exception e) {
            Debug.LogError(e);
        }
        isJoining = false;
    }
}
