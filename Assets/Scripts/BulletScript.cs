using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [HideInInspector] public GameObject targetObj;
    [SerializeField] private float moveSpeed = 20f;
    [SerializeField] private int bulletDamage = 50;

    private GameMaster gm;
    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
    }

    private void FixedUpdate()
    {
        if (targetObj != null)
        {
            gameObject.transform.position = Vector3.MoveTowards(transform.position, targetObj.transform.position, moveSpeed * Time.fixedDeltaTime);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            GiveDamageTo(other);
        }
    }

    private void GiveDamageTo(Collider enemy)
    {
        //gm.PlayEnemyDeathPFX(enemy.gameObject.transform.position);
        //Destroy(enemy.gameObject);
        enemy.gameObject.GetComponent<EnemyHealthSystem>().TakeDamage(bulletDamage);
        Destroy(gameObject);
    }


}
