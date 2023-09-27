using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Unity.Services.Core;
using UnityEngine.SceneManagement;

public class ClientGameManager {

    private const string menuSceneName = "Menu";
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
}
