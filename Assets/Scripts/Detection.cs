using System.Collections.Generic;
using UnityEngine;

public class Detection : MonoBehaviour
{
    private GameMaster gm;

    private void Start()
    {
        gm = GameObject.Find("GameMaster").GetComponent<GameMaster>();
    }

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

        if (other.gameObject.tag == "Player" && detectionState == DetectionState.DetectSolider && gm.levelState == GameMaster.LevelState.NotFinished)
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

    private void Update()
    {
        if (gm.levelState == GameMaster.LevelState.Finished && transform.parent.gameObject.tag == "Enemy")
        {
            targets = gm.solidersList;
        }
    }
}