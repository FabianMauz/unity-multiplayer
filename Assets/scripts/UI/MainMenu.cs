using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class MainMenu : MonoBehaviour{
  
  [SerializeField] 
  private TMP_InputField joinCode;
    public async void startHost() {
        await HostSingelton.Instance.createHost();
    }

    public async void StartClient(){
          await ClientSingelton.Instance.gameManager.startClientAsync(joinCode.text);
    }
}
