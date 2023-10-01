using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Services.Authentication;
using Unity.Services.Core;
using UnityEngine;

public static class AthentificationWrapper {
    public static AuthentificationState authState { get; private set; } = AuthentificationState.NOT_AUTHENTICATED;

    public static async Task<AuthentificationState> doAuth(int attemps = 5) {
        if (authState == AuthentificationState.AUTHENTICATED){ return authState; }

        if (authState == AuthentificationState.AUTHENTICATING) {
            Debug.Log("Already authentificating");
            await authenticating();
            return authState;
        }
        await handleSighIn(attemps);

        return authState;
            
    }

    private static async Task<AuthentificationState> authenticating() {
        while (authState==AuthentificationState.AUTHENTICATING||authState==AuthentificationState.NOT_AUTHENTICATED) {
            await Task.Delay(200);
        }

        return authState;

    }

    private static async Task handleSighIn(int attemps) {
        authState = AuthentificationState.AUTHENTICATING;
        int tries = 0;
        while (authState == AuthentificationState.AUTHENTICATING && tries < attemps) {

            try {
                await AuthenticationService.Instance.SignInAnonymouslyAsync();

                if (AuthenticationService.Instance.IsSignedIn && AuthenticationService.Instance.IsAuthorized) {
                    authState = AuthentificationState.AUTHENTICATED;
                    break;
                }
            } catch(Exception e) {
                Debug.LogError(e);
                authState = AuthentificationState.ERROR;
            }


            tries++;
            await Task.Delay(1000);

            if (authState != AuthentificationState.AUTHENTICATED) {
                Debug.Log("To many tries at authentificating " + tries);
                authState = AuthentificationState.TIME_OUT;
            }
        }
    }
}

public enum AuthentificationState {
    NOT_AUTHENTICATED,
    AUTHENTICATING,
    AUTHENTICATED,
    ERROR,
    TIME_OUT

}
