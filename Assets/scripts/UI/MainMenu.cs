using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour{
  
    public async void startHost() {
        await HostSingelton.Instance.createHost();
    }
}
