using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class BuildingSpawner : NetworkBehaviour {

    public List<GameObject> buildingPrefabs;

    public Vector2 gridDimentions;
    public Vector2 gridOffset;

    List<bool[,]> buildingGrid = new List<bool[,]>();

    public override void OnStartServer()
    {
        buildingGrid.Add(new bool[(int)gridDimentions.x, (int)gridDimentions.y]);
        buildingGrid.Add(new bool[(int)gridDimentions.x, (int)gridDimentions.y]);
    }

    public void buildBuilding(int buildingID, int playerID,Vector2 topLeftSpawnPosition)
    {
        Debug.Log("Player " + playerID + " is trying to build " + buildingID + " at location: " + topLeftSpawnPosition);
        if (checkGridDimention(buildingID, playerID, topLeftSpawnPosition))
        {
            placeBuilding(buildingID, playerID, topLeftSpawnPosition);
        }
    }


    bool checkGridDimention(int buildingID, int playerID, Vector2 topLeftSpawnPosition)
    {
        Vector2 buildingGridSize = buildingPrefabs[buildingID].GetComponent<Building>().getGridSize();

        try
        {
            int x = 0;
            while (x < buildingGridSize.x)
            {
                if (buildingGrid[playerID][(int)topLeftSpawnPosition.x + x, (int)topLeftSpawnPosition.y] == true)
                {
                    return false;
                }

                int y = 0;
                while (y < buildingGridSize.y)
                {
                    if (buildingGrid[playerID][(int)topLeftSpawnPosition.x + x, (int)topLeftSpawnPosition.y + y] == true)
                    {
                        return false;
                    }
                    y++;
                }

                x++;
            }
        }
        catch(Exception e)
        {
            return false;
        }

        return true;
    }

    void placeBuilding(int buildingID, int playerID, Vector2 topLeftSpawnPosition)
    {
        Vector2 buildingGridSize = buildingPrefabs[buildingID].GetComponent<Building>().getGridSize();

        //set grid positions to filled
        int x = 0;
        while (x < buildingGridSize.x)
        {
            buildingGrid[playerID][(int)topLeftSpawnPosition.x + x, (int)topLeftSpawnPosition.y] = true;

            int y = 0;
            while (y < buildingGridSize.y)
            {
                buildingGrid[playerID][(int)topLeftSpawnPosition.x + x, (int)topLeftSpawnPosition.y + y] = true;
                y++;
            }

            x++;
        }

        int xOffset = playerID == 1 ? (-2*(int)gridOffset.x) -2 : (int)gridOffset.x;

        //spawn the building
        GameObject temp = Instantiate(buildingPrefabs[buildingID], new Vector3(xOffset + (topLeftSpawnPosition.x + buildingGridSize.x/2), -gridOffset.y + (-topLeftSpawnPosition.y - buildingGridSize.y / 2), 0), Quaternion.identity);
        NetworkServer.Spawn(temp);

        temp.GetComponent<Building>().RpcSetColor(GetComponent<Main>().getPlayerColor(playerID));
    }


}
