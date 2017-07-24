using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Building : NetworkBehaviour
{

    public Vector2 gridSize;

    public float initialIncome;
    float currentIncome;

    public override void OnStartServer()
    {
        currentIncome = initialIncome;
    }

    public float getIncomeAmount()
    {
        return currentIncome;
    }

    public Vector2 getGridSize()
    {
        return gridSize;
    }

    [ClientRpc]
    public virtual void RpcSetColor(Color inputColor)
    {
        GetComponent<SpriteRenderer>().color = inputColor;
    }
}
