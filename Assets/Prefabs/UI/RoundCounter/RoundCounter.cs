using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class RoundCounter : NetworkBehaviour {

    public Text counterText;
    float timer = 0;

    public Action startCallback;

    [ClientRpc]
    public void RpcResetTimer(float time)
    {
        StartCoroutine(countDown(time));
    }


    IEnumerator countDown(float time)
    {
        timer = time;
        while(timer > 0)
        {
            counterText.text = Mathf.CeilToInt(timer) + "";
            yield return null;
            timer -= Time.deltaTime;
        }

        if(isServer)
        {
            startCallback();
        }
    }
}
