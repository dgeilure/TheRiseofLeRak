using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //author: daniela

    // TODOs

    // ATTACK: one attack, big damage
            // DONE --> set health of enemy lower
            // --> (if enemy is not protecting itself; similar to protection spell here in the playercontroller)

    // MALICE: multiple times for a certain amount of time,
            // DONE --> coroutine - every x seconds damage to enemy -> set health lower
    // less damage in a hit but similar damage to attack if all damages are added together
        // adds to own health the amount that went down
            // DONE --> same cor as above -> add same amount of health points to player health that are subtracted 

    // PROTECTION spell:
    // heal own health (maybe like a quarter) 
            // DONE --> get max health in the beginning, add a certain amount after spell is executed if the health is not full
    // stop the next x attacks (proposition: two!)
            // --> int = 2, enemy controller: check if int = 0, if yes attack, if no int -= 1

    // mana regen over time
            // DONE --> coroutine: regen every x seconds x amount until it is full (while mana less than max mana: add certain amount of mana, waitforseconds)
            //done muss nur noch aufgerufen werden

    //VARIABLES

    [SerializeField]
    public EnemyController enemyController;

    public CharacterStats playerStats;

    private int playerMaxHealth;
    private int playerMaxMana;

    private int playerCurrentHealth;

    private int playerHeal;
    private int spellManaCost = 5;
    private int attackStrength = 20;
    private int maliceStrength = 2; //but repeats ten times -> 10 seconds

    private Coroutine maliceCor;

    //METHODS

    private void Start()
    {
        playerStats = new CharacterStats();

        playerMaxHealth = playerStats.getHealth();
        playerMaxMana = playerStats.getMana();

        playerHeal = playerMaxHealth / 4; // 1 fourth of health gets healed

        StartCoroutine(RegenerateMana(2,3)); //3 mana every 2 seconds
    }

    //---

    public void playerProtection() // to do checkup if this makes sense and works
    {
        if (playerStats.getMana() > spellManaCost)
        {
            playerCurrentHealth = playerStats.getHealth();

            HealPlayer(playerHeal);

            Debug.Log("PLAYER PROTECT, Health: " + playerStats.getHealth());

            SubtractMana();
        }
    }

    public void playerAttack()
    {
        if (playerStats.getMana() > spellManaCost)
        {
            int newEnemyHealth = enemyController.enemyStats.getHealth() - attackStrength;

            if (newEnemyHealth > 0)
            {
                enemyController.enemyStats.setHealth(newEnemyHealth);
            }
            else
            {
                enemyController.enemyStats.setHealth(0);
            }

            Debug.Log("PLAYER ATTACK, Enemy Health: " + enemyController.enemyStats.getHealth());

            SubtractMana();
        }
    }

    public void playerMalice()
    {
        if (playerStats.getMana() > spellManaCost)
        {
            StopMaliceCor();
            maliceCor = StartCoroutine(PlayerMalice(10)); //10 iterations

            Debug.Log("PLAYER MALICE");

            SubtractMana();
        }
    }

    //---

    private void SubtractMana()
    {
        int newMana = playerStats.getMana() - spellManaCost;

        if (newMana > 0)
        {
            playerStats.setMana(newMana);
        }
        else
        {
            playerStats.setMana(0);
        }

        Debug.Log("Player Mana:" + playerStats.getMana());
    }    
    
    private void AddMana(int amount)
    {
        int newMana = playerStats.getMana() + amount;

        if (newMana < playerMaxMana)
        {
            playerStats.setMana(newMana);
        }
        else
        {
            playerStats.setMana(playerMaxMana);
        }

        Debug.Log("Player Mana:" + playerStats.getMana());
    }

    private void HealPlayer(int healthIncrease)
    {
        if ((playerCurrentHealth + healthIncrease) < playerMaxHealth)
        {
            playerStats.setHealth(playerCurrentHealth + healthIncrease);
        }
        else
        {
            playerStats.setHealth(playerMaxHealth);
        }
    }

    //coroutine
    //iterations: amount of times the enemy gets damaged + player gets healed

    private IEnumerator PlayerMalice(int iterations)
    {
        int enemyCurrentHealth;
        for (int i = 0; i < iterations; i++)
        {
            enemyCurrentHealth = enemyController.enemyStats.getHealth();
            if ((enemyCurrentHealth - maliceStrength) > 0)
            {
                enemyController.enemyStats.setHealth(enemyCurrentHealth - maliceStrength);
                HealPlayer(maliceStrength);
            }
            else
            {
                enemyController.enemyStats.setHealth(0);
                //does not heal if enemy dies from the attack! because.. like why lol you already won
            }
            yield return new WaitForSecondsRealtime(1);
        }
    }

    //coroutine
    //buffer: time between mana regeneration
    //mana: amount of mana that gets regenerated

    private IEnumerator RegenerateMana(float buffer, int mana)
    {
        AddMana(mana);
        yield return new WaitForSecondsRealtime(buffer);
    }

    private void StopMaliceCor()
    {
        if (maliceCor != null) StopCoroutine(maliceCor);
    }
}
