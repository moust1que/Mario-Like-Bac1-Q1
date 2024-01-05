using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public LayerMask playerLayer;
    public int bonus;

    void OnCollisionEnter(Collision collision)
    {
        if (IsPlayerCollision(collision))
        {
            if (IsCollisionFromBottom(collision))
            {
                if (bonus == 0)
                {
                    Destroy(gameObject);
                }
                else if (bonus == 1)
                {
                    Debug.Log("Block piece !");
                }
                else if (bonus == 2)
                {
                    Debug.Log("Block champi !");
                }
                else
                {
                    Debug.Log("Autres block !");
                }
            }
        }
    }

    bool IsPlayerCollision(Collision collision)
    {
        return (playerLayer.value & 1 << collision.gameObject.layer) > 0;
    }

    bool IsCollisionFromBottom(Collision collision)
    {
        foreach (ContactPoint contact in collision.contacts)
        {
            if (contact.normal.y > 0.8f)
            {
                return true;
            }
        }
        return false;
    }
}