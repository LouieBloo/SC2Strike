using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Economy : NetworkBehaviour {

    float[] playerGas = new float[2];
    

    PlayerController[] players;
	
	public void startEconomyUpdates(PlayerController[] players)
    {
        this.players = players;

        StartCoroutine(economyTick());
    }

    IEnumerator economyTick()
    {
        while (true)
        {
            yield return new WaitForSeconds(2);

            int x = 0;
            while (x < players.Length)
            {
                playerGas[x] += 10;
                players[x].GetComponent<GasCounter>().playerGas = playerGas[x];
                x++;
            }
        }
    }
}
