using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstSolider : MonoBehaviour
{

    [SerializeField] private GameMaster gm;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        gm.solidersList.Add(gameObject);
        gm.UpdateSoliderCount();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            gm.playerState = GameMaster.PlayerState.Move;
            rb.useGravity = false;
            rb.constraints = RigidbodyConstraints.FreezePositionY;

            Destroy(this, 1);
        }
    }
}
