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

public class HostGameManager {
    private Allocation allocation;
    private String joinCode;
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
        RelayServerData data = new RelayServerData(allocation,"udp");
        unityTransport.SetRelayServerData(data);
        NetworkManager.Singleton.StartHost();
        NetworkManager.Singleton.SceneManager.LoadScene("Game",UnityEngine.SceneManagement.LoadSceneMode.Single);

    }
}
