using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public GameManager gameManager;
    public LayerMask playerLayer;
    public int bonus;


    void OnCollisionEnter(Collision collision)
    {
        if (IsPlayerCollision(collision)) //Detecte si c'est le joueur qui collisionne
        {
            if (IsCollisionFromBottom(collision)) //Detecte si la collision vient du dessous
            {
                if (bonus == 1) //Block cassable
                {
                    Destroy(gameObject);
                }
                else if (bonus == 2) //Block piece
                {
                    gameManager.AddPiece(); //Ajout de 1 piece
                    gameManager.AddScore(100); //Ajout du score
                    bonus = 0;
                }
                else if (bonus == 3) //Block champi
                {
                    Debug.Log("Block champi !");
                }
            }
        }
    }

    //Fonction de detection si c'est le joueur qui collisionne
    bool IsPlayerCollision(Collision collision)
    {
        return (playerLayer.value & 1 << collision.gameObject.layer) > 0;
    }

    //Fonction de detection si la collision vient du dessous
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