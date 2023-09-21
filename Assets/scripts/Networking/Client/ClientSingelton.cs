using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class ClientSingelton : MonoBehaviour{

    private static ClientSingelton instance;
    private ClientGameManager gameManager;

    public static ClientSingelton Instance { get {
            if (instance != null) { return instance; }
            instance = FindAnyObjectByType<ClientSingelton>();
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
        gameManager = new ClientGameManager();
        await gameManager.initAsync();
    }

}
