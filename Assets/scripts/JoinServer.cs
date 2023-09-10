using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class JoinServer : MonoBehaviour{
    public void join() {
        NetworkManager.Singleton.StartClient();
    }
}
