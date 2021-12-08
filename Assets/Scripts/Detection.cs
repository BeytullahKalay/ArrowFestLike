using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detection : MonoBehaviour
{
    public DetectionState detectionState;

    public enum DetectionState
    {
        DetectEnemy,
        DetectSolider,
    }



    public List<GameObject> targets = new List<GameObject>();   

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy" && detectionState == DetectionState.DetectEnemy)
        {
            targets.Add(other.gameObject);
        }

        if (other.gameObject.tag == "Player" && detectionState == DetectionState.DetectSolider)
        {
            targets.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Enemy" && detectionState == DetectionState.DetectEnemy)
        {
            targets.Remove(other.gameObject);
        }
    }
}