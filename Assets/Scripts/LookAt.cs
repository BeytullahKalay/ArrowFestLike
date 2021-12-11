using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{
   private List<GameObject>Arms;
   private GameMaster gm;

    private float lookSpeed;
    private float handsUpSpeed;
    private Detection detectScript;


    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();

        detectScript = transform.GetChild(0).GetComponent<Detection>();
        Arms = new List<GameObject>();
        Arms.Add(transform.GetChild(1).gameObject);
        Arms.Add(transform.GetChild(2).gameObject);

        lookSpeed = gm.lookAtLerpSpeed;
        handsUpSpeed = gm.handUpLerpSpeed;

    }

    void FixedUpdate()
    {
        if (detectScript.targets.Count > 0)
        {
            if (detectScript.targets[0] != null)
            {
                LookAtPlayer();
                if(gameObject.CompareTag("Enemy"))
                    HandsUpBois();
            }
            else
            {
                ClearDetectedEnemList();
            }
        }
        else
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.identity, lookSpeed * Time.deltaTime);
        }
    }



    private void LookAtPlayer()
    {
        Vector3 lookDir = detectScript.targets[0].transform.position - transform.position;
        Quaternion desRot = Quaternion.LookRotation(lookDir);
        transform.rotation = Quaternion.Lerp(transform.rotation, desRot, lookSpeed * Time.fixedDeltaTime);
    }

    private void HandsUpBois()
    {   
        foreach (GameObject arm in Arms)
        {
            Quaternion desRot = arm.transform.rotation * Quaternion.Euler(-90, 0, 0);
            arm.transform.rotation = Quaternion.Lerp(transform.rotation, desRot, handsUpSpeed * Time.fixedDeltaTime);
        }
    }


    private void ClearDetectedEnemList()
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
