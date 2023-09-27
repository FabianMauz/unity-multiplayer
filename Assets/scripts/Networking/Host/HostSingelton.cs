using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class HostSingelton : MonoBehaviour{

    private static HostSingelton instance;
    private HostGameManager gameManager;

    public static HostSingelton Instance { get {
            if (instance != null) { return instance; }
            instance = FindAnyObjectByType<HostSingelton>();
            if (instance == null) {
                print("Error in client sigelton inzantion");
                return null;
            }
            return instance;
        }
    }
    
    void Start(){
        DontDestroyOnLoad(gameObject);
    }

    public async Task createClient() {
        gameManager = new HostGameManager();
        await gameManager.initAsync();
    }

}
