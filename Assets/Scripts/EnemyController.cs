using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    //authors: saskia & tobi & daniela

    //TO DO 
    // wenn von malice getroffen: eher protection

    // attack pattern testen

    // vererbung von spell aufrufen 

    // game over

    [SerializeField]
    private PlayerController playerController;

    public CharacterStats enemyStats;

    public bool fightStarted = false;

    private int enemyMaxHealth;
    private int enemyCurrentHealth;

    private int enemyHeal;

    private int attackStrength = 20;
    private int maliceStrength = 2; //but repeats ten times -> 10 seconds

    private Coroutine maliceCor;

    private string[] lowHealthSpells;
    private string[] midHealthSpells;
    private string[] highHealthSpells;


    private void Start()
    {
        enemyStats = new CharacterStats(200,200);
        
        enemyMaxHealth = enemyStats.getHealth();

        enemyHeal = enemyMaxHealth / 4; // 1 fourth of health gets healed when you heal

        lowHealthSpells = generateSpellProbabilityArrays(10,50,40);
        midHealthSpells = generateSpellProbabilityArrays(40,30,30);
        highHealthSpells = generateSpellProbabilityArrays(70,10,20);
    }

    private string [] generateSpellProbabilityArrays(int probabilityA, int probabilityP, int probabilityM) 
    {
        string [] spellArray = {};

        // probability of each attack in %
        // sums up to 100 (%)
        int attackProbability = probabilityA;
        int protectionProbability = probabilityP;
        int maliceProbability = probabilityM;

        // fills spellArray with spells 
        fillSpellArray (spellArray, attackProbability, "Attack"); // Attack Spell
        fillSpellArray (spellArray, protectionProbability, "Protection"); // Protection Spell
        string[] arr = fillSpellArray (spellArray, maliceProbability, "Malice"); // Malice Spell

        return arr;
    }

    // Das LeRak-Gefühl ist das, was uns alle zusammenhält
    // The fight pattern for the enemy
    public void fight()
    {
        Debug.Log("FIGHT INIT");

        int enemyCurrentHealth = enemyStats.getHealth(); //the current health of the enemy

        // LeRak does not have a mana limitation
        // As the fight pattern of LeRak limits how fast he can cast spells, no extra limitation is needed.
        // The time LeRak waits between spells is a delay that prevents him from attacking too fast
        // A limited mana level for LeRak would not be visible to the player anyway 

        // random number to choose spell
        int random = Random.Range(0, 100);
        string spell = "";
        float delay = 1f;

        // probabilities depanding on health
        if (enemyCurrentHealth > 150)       // good health
        {
            Debug.Log("Enemy health is good");
            spell = highHealthSpells[random];
            delay = calculateDelay(7.0f);
        }
        else if (enemyCurrentHealth > 50)   // medium health
        {
            Debug.Log("Enemy health is okay");
            spell = midHealthSpells[random];
            delay = calculateDelay(14.0f);
        }
        else if (enemyCurrentHealth > 0)    // low health
        {
            Debug.Log("Enemy health is bad");
            spell = lowHealthSpells[random];
            delay = calculateDelay(2.0f);
        }
        Debug.Log("LeRak chose this spell: " + spell);

        //call corresponding method
        switch(spell)
        {
            case "Attack":
                enemyAttack();
                delay = modifyDelay(delay, 2.0f);
                break;
            case "Protection":
                enemyProtection();
                delay = modifyDelay(delay, 1.0f);
                break;
            case "Malice":
                enemyMalice();
                delay = modifyDelay(delay, 3.5f);
                break;
            default:
                Debug.Log("Fuck (something went wrong)");
                break;
        }

        //  recursive
        if (enemyCurrentHealth != 0 && (playerController.playerStats.getHealth()) != 0)
        {
            //waits a "delay" amount of time before calling the next round of fight
            Invoke(nameof(fight), delay);
        } // else game over, but we don't have that yet
    }

    //modifes delay depending on the spell that gets cast
    private float modifyDelay(float currentDelay, float spellModifier)
    {
        return currentDelay + spellModifier;
    }
   
    //sets base delay depending on the health of enemy
    private float calculateDelay(float healthModifier) 
    {
        float delay = (Random.Range(5.0f, 10.0f)) + healthModifier;
        return delay;
    }

    // fills spellArray with spells
    private string[] fillSpellArray(string[] spellArray, int spellProbability, string spellIdentifier)
    {
        for (int i = 0; i < spellProbability; i++)
        {
            spellArray[spellArray.Length] = spellIdentifier;
        }
        return spellArray;
    }



    // spell functions:
    // this should all be inherited from another class "SpellStats" or something alike in the future, to avoid doubling of every single spell function
    public void enemyProtection() // to do checkup if this makes sense and works
    {
        enemyCurrentHealth = enemyStats.getHealth();

        HealEnemy(enemyHeal);

        Debug.Log("ENEMY PROTECT, Health: " + enemyStats.getHealth());    
    }

    public void enemyAttack()
    {
        int newPlayerHealth = playerController.playerStats.getHealth() - attackStrength;

        if (newPlayerHealth > 0)
        {
            playerController.playerStats.setHealth(newPlayerHealth);
        }
        else
        {
            playerController.playerStats.setHealth(0);
        }

        Debug.Log("ENEMY ATTACK, Player Health: " + playerController.playerStats.getHealth());
    }

    public void enemyMalice()
    {
        StopMaliceCor();
        maliceCor = StartCoroutine(EnemyMalice(10)); //10 iterations

        Debug.Log("ENEMY MALICE");
    }

    // heals the enemy
    private void HealEnemy(int healthIncrease)
    {
        if ((enemyCurrentHealth + healthIncrease) < enemyMaxHealth)
        {
            enemyStats.setHealth(enemyCurrentHealth + healthIncrease);
        }
        else
        {
            enemyStats.setHealth(enemyMaxHealth);
        }
    }

    private void StopMaliceCor()
    {
        if (maliceCor != null) StopCoroutine(maliceCor);
    }

    //coroutine
    //iterations: amount of times the player gets damaged + enemy gets healed

    private IEnumerator EnemyMalice(int iterations)
    {
        int playerCurrentHealth;
        for (int i = 0; i < iterations; i++)
        {
            playerCurrentHealth = playerController.playerStats.getHealth();
            if ((playerCurrentHealth - maliceStrength) > 0)
            {
                playerController.playerStats.setHealth(playerCurrentHealth - maliceStrength);
                HealEnemy(maliceStrength);
            }
            else
            {
                playerController.playerStats.setHealth(0);
                //does not heal if player dies from the attack! because.. like why lol you already won
            }
            yield return new WaitForSecondsRealtime(1);
        }
    }

    /*
    private void detectHarms ()
    {
        if (siehtAusWieHerrHarms == true){
            difficulty = ultra;
        }
    }
    */
}
