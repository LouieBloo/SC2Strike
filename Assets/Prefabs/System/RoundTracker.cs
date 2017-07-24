using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class RoundTracker : NetworkBehaviour
{

    public RoundCounter roundCounter;
    public float roundTime;


    PlayerController[] players;

	public void startGame(PlayerController[] players)
    {
        this.players = players;

        roundCounter.startCallback = roundStartCallback;
        roundCounter.RpcResetTimer(roundTime);
    }

    //called when the round starts
    void roundStartCallback()
    {
        roundCounter.RpcResetTimer(roundTime);
    }
}
