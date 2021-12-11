using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OwnedSoliderController : MonoBehaviour
{
    GameMaster gm;

    private void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("FinishLine"))
        {
            gm.levelState = GameMaster.LevelState.Finished;
            gm.playerState = GameMaster.PlayerState.Stopped;
        }

        if (other.gameObject.tag == "Wall")
        {
            other.gameObject.GetComponent<WallOperation>().DoOperation();
            Destroy(other.gameObject);
        }
    }
}
