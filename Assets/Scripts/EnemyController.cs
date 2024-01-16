using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    //TO DO 
    // attack pattern

    [SerializeField]
    private PlayerController playerController;

    public CharacterStats enemyStats;

    private void Start()
    {
        enemyStats = new CharacterStats(200,200);
    }
}
