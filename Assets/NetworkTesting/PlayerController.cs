using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour {

    public GameObject testUnit;
    public GameObject spawner;
    public GameObject BuildingPanelPrefab;

    [SyncVar]
    public int playerID;

    [SyncVar(hook = "ColorChange")]
    public Color spriteColor;

    void Start()
    {
        if (!isLocalPlayer)
        {
            Destroy(GetComponent<Touch>());
        }
    }

    void Update()
    {
        if(!isLocalPlayer)
        {
            return;
        }
        

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Space");
            //CmdFire();
        }
    }


    public override void OnStartLocalPlayer()
    {
        CmdSetup();
        GlobalVariables.variables.LocalPlayer = this.gameObject;
    }

    public void buildingClicked(int buildingID, Vector2 position)
    {
        CmdBuildBuildingOnServer(buildingID,position);
    }

    [Command]
    void CmdBuildBuildingOnServer(int buildingID, Vector2 position)
    {
        GlobalVariables.variables.BuildingSpawner.buildBuilding(buildingID, playerID, position);
    }

    [Command]
    void CmdSetup()
    {
        //GameObject temp = Instantiate(spawner, new Vector3(-8, Random.Range(-5,5), 0), Quaternion.identity);
        //NetworkServer.SpawnWithClientAuthority(temp, connectionToClient);

        //spriteColor = new Color(Random.Range(0.0F, 1.0F), Random.Range(0.0F, 1.0F), Random.Range(0.0F, 1.0F));
        //GameObject temp = Instantiate()
    }


    public void StartGame(int playerID)
    {
        //if (!isLocalPlayer) { return; }
        this.playerID = playerID;
        //this.playerID = playerID;
    }

    [ClientRpc]
    public void RpcChangeColor()
    {
        if (!isLocalPlayer) { return; }

        spriteColor = new Color(Random.Range(0.0F, 1.0F), Random.Range(0.0F, 1.0F), Random.Range(0.0F, 1.0F));

        if(!isServer)
        {
            CmdSendColor(spriteColor);
        }
    }

    [Command]
    void CmdSendColor(Color input)
    {
        spriteColor = input;
    }

    public void ColorChange(Color input)
    {
        if (hasAuthority)
        {
        }
        GetComponent<SpriteRenderer>().color = input;
    }

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

    // This [Command] code is called on the Client …
    // … but it is run on the Server!
    //[Command]
    //void CmdFire()
    //{
    //    // Create the Bullet from the Bullet Prefab
    //    var bullet = (GameObject)Instantiate(
    //        bulletPrefab,
    //        bulletSpawn.position,
    //        bulletSpawn.rotation);

    //    // Add velocity to the bullet

    //    //bullet.GetComponent<Rigidbody2D>().AddForce(transform.up * 150);
    //    //bullet.GetComponent<Rigidbody2D>().velocity = new Vector3(0, 10, 0);
    //    NetworkServer.Spawn(bullet);


    //    // Destroy the bullet after 2 seconds
    //    Destroy(bullet, 2.0f);
    //}
}
