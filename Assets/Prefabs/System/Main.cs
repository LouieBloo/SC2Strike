using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using PathFind;

public class Main : NetworkBehaviour {

    public RoundTracker roundTracker;
    public Economy economy;

    PlayerController[] players;

    int playerCount = 0;

    void Start()
    {
        
    }




    public override void OnStartServer()
    {
        StartCoroutine(waitForPlayers());   
    }

    IEnumerator waitForPlayers()
    {
        while(NetworkServer.connections.Count != 2)
        {
            yield return null;
        }

        yield return new WaitForSeconds(1);


        players = new PlayerController[2];

        //get players, set random color, also assign IDS
        int id = 0;
        GameObject[] playerObjects = GameObject.FindGameObjectsWithTag("Player");
        foreach(GameObject gm in playerObjects)
        {
            gm.GetComponent<PlayerController>().RpcChangeColor();
            gm.GetComponent<PlayerController>().StartGame(id);
            id++;
        }

        players[0] = playerObjects[0].GetComponent<PlayerController>();
        players[1] = playerObjects[1].GetComponent<PlayerController>();


        //start game
        roundTracker.startGame(players);
        economy.startEconomyUpdates(players);
    }

    public Color getPlayerColor(int playerID)
    {
        if(players == null || playerID >= players.Length) { return Color.white; }
        return players[playerID].spriteColor;
    }
}

