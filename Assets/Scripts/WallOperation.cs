using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WallOperation : MonoBehaviour
{
    [SerializeField] private int operationValue;

    [SerializeField] private Transform rightCornerCheck;
    [SerializeField] private Transform lefCornerCheck;
    public LayerMask whatIsGround;

    [SerializeField] private float moveSpeed = 5f;

    Text textUI;
    GameMaster gm;

    bool goingRight = false;

    public Operation operation;
    public enum Operation
    {
        Addition,
        Subtraction,
        Multiplication,
        Divide,
    }

    public MoveState moveState;
    public enum MoveState
    {
        Static,
        Movable
    }


    private void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();

        textUI = transform.GetChild(0).transform.GetChild(0).GetComponent<Text>();
        switch (operation)
        {
            case Operation.Addition:
                SetWallForAddition();
                break;
            case Operation.Subtraction:
                SetWallForSubtraction();
                break;
            case Operation.Multiplication:
                SetWallForMultiplication();
                break;
            case Operation.Divide:
                SetWallForDivide();
                break;
            default:
                Debug.LogError("Undefined Operation");
                break;
        }
    }



    private void SetWallForAddition()
    {
        textUI.text = "+" + operationValue;
    }

    private void SetWallForSubtraction()
    {
        textUI.text = "-" + operationValue;
    }

    private void SetWallForMultiplication()
    {
        textUI.text = "x" + operationValue;
    }

    private void SetWallForDivide()
    {
        textUI.text = "÷" + operationValue;
    }

    public void DoOperation()
    {
        switch (operation)
        {
            case Operation.Addition:
                Addition();
                break;
            case Operation.Subtraction:
                Subtraction();
                break;
            case Operation.Multiplication:
                Multiplication();
                break;
            case Operation.Divide:
                Divide();
                break;
            default:
                break;
        }
        gm.UpdateSoliderCount();
    }

    #region Operation Definitions

    private void Addition()
    {
        for (int i = 0; i < operationValue; i++)
        {
            SpawnASolider();
        }
    }

    private void Subtraction()
    {
        if (gm.solidersList.Count > operationValue)
        {
            RemoveObjectFromEndOfList(operationValue);
        }
        else
        {
            int listCount = gm.solidersList.Count;
            for (int i = 1; i < listCount; i++)
            {
                GameObject removedObj = gm.solidersList[1];
                gm.solidersList.Remove(removedObj);
                Destroy(removedObj);
            }
        }
    }

    private void Multiplication()
    {
        int currentListCount = gm.solidersList.Count;
        int multipliResult = operationValue * currentListCount;
        multipliResult -= currentListCount;

        print(currentListCount);
        for (int i = 0; i < multipliResult; i++)
        {
            SpawnASolider();
        }
    }

    private void Divide()
    {
        int currentListCount = gm.solidersList.Count;
        int divideResult = currentListCount / operationValue;

        if (divideResult > 0)
        {
            RemoveObjectFromEndOfList(divideResult);
        }

    }

    #endregion


    #region Spawn and Remove Functions

    private void SpawnASolider()
    {
        float r = 2 * Mathf.Sqrt(Random.value);
        float theta = Random.value * 2 * Mathf.PI;
        float x = gm.collectedSolidersHolder.position.x + r * Mathf.Cos(theta);
        float z = gm.collectedSolidersHolder.position.z + r * Mathf.Sin(theta);


        Vector3 finalSpawnPos = new Vector3(x, gm.collectedSolidersHolder.position.y, z);
        GameObject obj = Instantiate(gm.soliderPrefab, finalSpawnPos, Quaternion.identity);
        obj.transform.parent = gm.collectedSolidersHolder.transform;

        gm.solidersList.Add(obj);
    }

    private void RemoveObjectFromEndOfList(int removeCount)
    {
        for (int i = 1; i < removeCount + 1; i++)
        {
            GameObject removedGameObj = gm.solidersList[gm.solidersList.Count - i];
            gm.solidersList.Remove(removedGameObj);
            Destroy(removedGameObj);
        }
    }

    #endregion

    private void Update()
    {
        if (moveState == MoveState.Movable)
        {
            Ray ray_R = new Ray(rightCornerCheck.transform.position, Vector3.down);
            Ray ray_L = new Ray(lefCornerCheck.transform.position, Vector3.down);
            RaycastHit hitInfo;

            if (goingRight)
            {
                if (Physics.Raycast(ray_R, out hitInfo, 100, whatIsGround))
                {
                    Debug.DrawLine(ray_R.origin, hitInfo.point, Color.green);
                    transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
                }
                else
                {
                    goingRight = false;
                }
            }
            else
            {
                if (Physics.Raycast(ray_L,out hitInfo, 100, whatIsGround))
                {
                    Debug.DrawLine(ray_L.origin, hitInfo.point, Color.green);
                    transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
                }
                else
                {
                    goingRight = true;
                }
            }
        }
    }

}
