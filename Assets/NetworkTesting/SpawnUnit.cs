using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SpawnUnit : NetworkBehaviour {

    public GameObject testUnit;

    [SyncVar(hook = "ColorChange")]
    public Color spriteColor;

    void Start()
    {
       // Debug.Log("On start");
    }

    public override void OnStartClient()
    {
        //Debug.Log("On start client");
        if (isServer)
        {
            spriteColor = new Color(Random.Range(0.0F, 1.0F), Random.Range(0.0F, 1.0F), Random.Range(0.0F, 1.0F));
        }
    }

    public override void OnStartServer()
    {
       // Debug.Log("one start server");
    }

    

    public void spawn()
    {
        

        if (!hasAuthority) { return; }

        CmdSpawn();

        
    }

    [Command]
    void CmdSpawn()
    {
        GameObject temp = Instantiate(testUnit, new Vector2(Random.Range(-5, 5), Random.Range(-5, 5)), Quaternion.identity);

        NetworkServer.Spawn(temp);

        RpcSpinAnimation();
    }

    [ClientRpc]
    void RpcSpinAnimation()
    {
        GetComponent<Animator>().SetTrigger("Fire");
    }

    

    public void ColorChange(Color input)
    {
        GetComponent<SpriteRenderer>().color = input;
    }
}
