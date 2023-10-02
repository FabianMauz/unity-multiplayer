using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Unity.Services.Core;
using Unity.Services.Relay;

using UnityEngine.SceneManagement;
using Unity.Services.Relay.Models;
using System;
using Unity.Netcode.Transports.UTP;
using Unity.Netcode;
using Unity.Networking.Transport.Relay;

public class ClientGameManager {

   private const string menuSceneName = "Menu";

   private JoinAllocation allocation;
   public async Task<bool> initAsync() {
        await UnityServices.InitializeAsync();
       AuthentificationState authState= await AthentificationWrapper.doAuth();
        if (authState == AuthentificationState.AUTHENTICATED) {
            return true;
        }
        return false;
    }

    public void goToMenu() {
        SceneManager.LoadScene(menuSceneName);
    }

    public async Task startClientAsync(string joinCode){

        try {
            allocation = await Relay.Instance.JoinAllocationAsync(joinCode);
        }catch(Exception e) {
            Debug.LogError(e);
            return;
        }

        UnityTransport unityTransport = NetworkManager.Singleton.GetComponent<UnityTransport>();
        RelayServerData data = new RelayServerData(allocation, "dtls");
        unityTransport.SetRelayServerData(data);
        NetworkManager.Singleton.StartClient();

        
    }
}
