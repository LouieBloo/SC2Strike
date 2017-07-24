using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Health : NetworkBehaviour
{
    
    public const float maxHealth = 100;
    [SyncVar(hook = "OnChangeHealth")]
    public float currentHealth = maxHealth;
    public RectTransform healthBar;


    public void TakeDamage(float amount)
    {
        if (!isServer) { return; }

        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            currentHealth = maxHealth;
            // called on the Server, but invoked on the Clients
            RpcRespawn();
            Debug.Log("Dead!");
        }

        
    }

    [ClientRpc]
    void RpcRespawn()
    {
        
        if (isLocalPlayer)
        {
            // move back to zero location
            transform.position = Vector3.zero;
        }
    }

    void OnChangeHealth(float currentHealth)
    {
        healthBar.sizeDelta = new Vector2(0 + ((currentHealth / maxHealth) * 0.4F), healthBar.sizeDelta.y);
    }
}