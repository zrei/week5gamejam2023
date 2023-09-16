using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Persisting
{
    // Start is called before the first frame update
    /*void Start()
    {
        GlobalEvents.PlayerEvents.PlayerHealthChangeEvent += ChangePlayerHealth;
        GlobalEvents.PlayerEvents.PlayerDeathEvent += PlayerDeath;

    }

    void OnDestroy()
    {
        GlobalEvents.PlayerEvents.PlayerHealthChangeEvent -= ChangePlayerHealth;
        GlobalEvents.PlayerEvents.PlayerDeathEvent -= PlayerDeath;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ChangePlayerHealth(float change)
    {
        playerHealth += change;
        if (playerHealth <= 0)
        {
            GlobalEvents.PlayerEvents.PlayerDeathEvent?.Invoke();
        }
    }

    private void PlayerDeath()
    {
        Debug.Log("Player died");
    }*/
}
