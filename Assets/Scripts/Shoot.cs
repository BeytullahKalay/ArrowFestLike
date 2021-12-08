using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    [SerializeField] private float firePerSecond = 1;
    [SerializeField] private Transform bulletSpawnPos;

    private Detection detectScript;

    private GameMaster gm;

    private float nextFireTime = 0;

    void Start()
    {
        detectScript = transform.GetChild(0).GetComponent<Detection>();
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
    }

    void Update()
    {
        if (detectScript.targets.Count > 0)
        {
            if (detectScript.targets[0] != null && Time.time > nextFireTime)
            {
                GameObject bulletObj = Instantiate(gm.bulletPrefab, bulletSpawnPos.position, Quaternion.identity);
                bulletObj.GetComponent<BulletScript>().targetObj = detectScript.targets[0];
                nextFireTime = Time.time + 1 / firePerSecond;
                print("Shoot");
            }
        }
        else
        {
            List<GameObject> removeObjectList = new List<GameObject>();
            foreach (GameObject item in detectScript.targets)
            {
                if (item == null)
                {
                    removeObjectList.Add(item);
                }
            }

            foreach (GameObject item in removeObjectList)
            {
                detectScript.targets.Remove(item);
            }
            removeObjectList.Clear();
        }
    }
}
