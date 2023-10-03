using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Unity.Services.Relay;
using System;
using Unity.Services.Relay.Models;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using Unity.Networking.Transport.Relay;
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;

public class HostGameManager {
    private Allocation allocation;
    private String joinCode;
    private String lobbyId;
    private int maxConnectoins = 20;
   public async Task startHostAsync() {
        try {
            allocation = await Relay.Instance.CreateAllocationAsync(maxConnectoins);
        }catch(Exception e) {
            Debug.LogException(e);
            return;
        }
        try {
            joinCode = await Relay.Instance.GetJoinCodeAsync(allocation.AllocationId);
            Debug.Log("JoinCode " + joinCode);
        } catch (Exception e) {
            Debug.LogException(e);
            return;
        }

        UnityTransport unityTransport = NetworkManager.Singleton.GetComponent<UnityTransport>();
        RelayServerData data = new RelayServerData(allocation,"dtls");
        unityTransport.SetRelayServerData(data);


        try {

            DataObject joinCodeObject = new DataObject(
                        visibility: DataObject.VisibilityOptions.Member,
                        value: joinCode);

            CreateLobbyOptions lobbyOptions = new CreateLobbyOptions();
            lobbyOptions.IsPrivate = false;
            lobbyOptions.Data = new Dictionary<string, DataObject>();
            lobbyOptions.Data.Add("JoinCode", joinCodeObject);

            Lobby lobby =  await Lobbies.Instance.CreateLobbyAsync(
                "My Lobby",
                maxConnectoins,
                lobbyOptions);

            lobbyId = lobby.Id;

            
            HostSingelton.Instance.StartCoroutine(heartBeatLobby(15));


        }catch(Exception e) {
            Debug.LogError(e);
            return;
        }


        NetworkManager.Singleton.StartHost();
        NetworkManager.Singleton.SceneManager.LoadScene("Game",UnityEngine.SceneManagement.LoadSceneMode.Single);

    }

    private IEnumerator heartBeatLobby(float waitTimeSeconds) {
        WaitForSecondsRealtime delay = new WaitForSecondsRealtime(waitTimeSeconds);
        while (true) {
            Debug.Log("Heart Beat " + new DateTime());
            Lobbies.Instance.SendHeartbeatPingAsync(lobbyId);
            yield return delay;
        }
    }
}
