using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 4f;
    [SerializeField] private float detectPlayerAreaRadius = 2f;
    [SerializeField] private GameMaster gm;

    private Detection detectPlayerScript;

    private void Start()
    {
        transform.GetChild(0).GetComponent<SphereCollider>().radius = detectPlayerAreaRadius;
        detectPlayerScript = transform.GetChild(0).GetComponent<Detection>();
    }

    private void FixedUpdate()
    {
        if (detectPlayerScript.targets.Count > 0)
        {
            if (detectPlayerScript.targets[0] != null)
            {
                transform.position = Vector3.MoveTowards(transform.position, detectPlayerScript.targets[0].transform.position, moveSpeed * Time.fixedDeltaTime);
            }
            else
            {
                List<GameObject> removeObjectList = new List<GameObject>();
                foreach (GameObject item in detectPlayerScript.targets)
                {
                    if (item == null)
                    {
                        removeObjectList.Add(item);
                    }
                }

                foreach (GameObject item in removeObjectList)
                {
                    detectPlayerScript.targets.Remove(item);
                }
                removeObjectList.Clear();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            gm.solidersList.Remove(other.gameObject);
            gm.PlaySoliderDeathPFX(other.gameObject.transform.position);
            Destroy(other.gameObject);

            if (gm.solidersList.Count == 0)
            {
                gm.playerState = GameMaster.PlayerState.Stopped;
                print("GameOver");
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.GetChild(0).transform.position, detectPlayerAreaRadius);
    }

}
