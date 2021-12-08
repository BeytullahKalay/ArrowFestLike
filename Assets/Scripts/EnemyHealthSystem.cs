using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthSystem : MonoBehaviour
{
    public EnemyType enemyType;
    public enum EnemyType
    {
        normalEnemy,
        giantEnemy,
    }

    [Space(10)]
    public GameMaster gm;

    [HideInInspector] public int currentHealth;

    private void Start()
    {
        if (enemyType == EnemyType.normalEnemy)
        {
            currentHealth = gm.normalEnemyHealth;
        }
        else if (enemyType == EnemyType.giantEnemy)
        {
            currentHealth = gm.giantEnemyHealth;
        }

        DefaultEnemyMat();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            gm.PlayEnemyDeathPFX(transform.position);
            Destroy(gameObject);
        }
        HurtEnemyMat();
    }

    private void DefaultEnemyMat()
    {
        gameObject.GetComponent<MeshRenderer>().material = gm.defaultEnemyMat;
    }

    private void HurtEnemyMat()
    {
        gameObject.GetComponent<MeshRenderer>().material = gm.hurtEnemyMat;
        Invoke("DefaultEnemyMat", gm.enemyBeenHurtedTime);
    }
}
