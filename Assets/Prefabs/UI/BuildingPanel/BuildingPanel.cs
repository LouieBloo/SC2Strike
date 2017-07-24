using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class BuildingPanel : NetworkBehaviour {

    public GameObject barracksPrefab;

    int gridOffset;
    int gridOffsetY;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void barracksClicked()
    {
        GlobalVariables.variables.LocalPlayer.GetComponent<PlayerController>().buildingClicked(0,new Vector2(gridOffset, gridOffsetY));
        gridOffset += 3;

        if(gridOffset == 9)
        {
            gridOffset = 0;
            gridOffsetY += 2;
        }
    }
}
