using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour{
  
    public async void startHost() {
Debug.Log(HostSingelton.Instance);

        await HostSingelton.Instance.createHost();
    }
}
