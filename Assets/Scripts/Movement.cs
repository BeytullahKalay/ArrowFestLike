using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] GameMaster gm;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float slideSpeed = 5f;


    bool isTouching;

    float touchPosX;

    void Update()
    {
        if (gm.levelState == GameMaster.LevelState.NotFinished)
        {
            GetInput();
        }
        else
        {
            isTouching = false;
        }
    }


    private void FixedUpdate()
    {
        if (gm.playerState == GameMaster.PlayerState.Move)
        {
            transform.position += Vector3.forward * moveSpeed * Time.fixedDeltaTime;

            if (isTouching)
            {
                touchPosX += Input.GetAxis("Mouse X") * slideSpeed * Time.fixedDeltaTime;
            }
        }
         
        transform.position = new Vector3(touchPosX, transform.position.y, transform.position.z);
    }


    private void GetInput()
    {
        if (Input.GetMouseButton(0))
        {
            isTouching = true;
        }
        else
        {
            isTouching = false;
        }

    }

}
