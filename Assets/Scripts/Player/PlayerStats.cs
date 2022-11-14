using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats instance;

    public int atkFlat;
    public int defFlat;
    public int atkCdFlat;
    public int dodgeCdFlat;
    public int dodgeTime;

    public int maxHealth = 100;
    public int currentHealth;

    public bool canAttack;
    public bool canDodge;
    public bool canMove;
    public bool isBlocking;


    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de PlayerStats dans la scène");
            return;
        }

        instance = this;
    }
}
