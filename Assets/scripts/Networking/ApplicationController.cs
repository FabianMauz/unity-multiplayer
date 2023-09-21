using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class ApplicationController : MonoBehaviour{
    [SerializeField]
    private ClientSingelton clientPrefab;
    private async void  Start(){
        DontDestroyOnLoad(gameObject);

        bool dedicatedServer = SystemInfo.graphicsDeviceType == UnityEngine.Rendering.GraphicsDeviceType.Null;
        await launchInMode(dedicatedServer);
    }

    private async Task launchInMode(bool dedicatedServer) {
        if (dedicatedServer) {

        }else {
            ClientSingelton clientSingelton = Instantiate(clientPrefab);
            await clientSingelton.createClient();
        }
    }

   
}
