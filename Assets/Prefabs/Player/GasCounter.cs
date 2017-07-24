using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class GasCounter : NetworkBehaviour {

    Text gasCounter;

    [SyncVar(hook = "GasChange")]
    public float playerGas;

    public override void OnStartLocalPlayer()
    {
        gasCounter = GlobalVariables.variables.gasCounter;
    }

    void GasChange(float input)
    {
        //if (isClient)
        //{
        //    Debug.Log("client");
        //}
        //if (isLocalPlayer)
        //{
        //    Debug.Log("player");
        //}
        //if (hasAuthority)
        //{
        //    Debug.Log("has authroity");
        //}
        //if (isServer)
        //{
        //    Debug.Log("server");
        //}

        if(!isLocalPlayer)
        {
            return;
        }
        gasCounter.text = input + " gas";
    }
}
