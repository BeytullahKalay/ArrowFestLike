using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private GameMaster gm;
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset = new Vector3 (0,4,-15);
    [SerializeField] private float lerpValue = 3;

    private void LateUpdate()
    {

        if (gm.levelState == GameMaster.LevelState.NotFinished)
        {
            Vector3 desPos = target.position + offset;
            desPos.x = 0;
            transform.position = Vector3.Lerp(transform.position, desPos, lerpValue);
        }
    }
}
