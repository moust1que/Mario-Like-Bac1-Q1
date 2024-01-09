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
                    bonus = 0;
                    Destroy(gameObject);
                    gameManager.AddScore(50); //Ajout du score
                }
                else if (bonus == 2) //Block piece
                {
                    bonus = 0;
                    gameManager.AddPiece(); //Ajout de 1 piece
                    gameManager.AddScore(200); //Ajout du score
                }
                else if (bonus == 3) //Block champi
                {
                    bonus = 0;
                    Debug.Log("Block champi !");
                }
            }
        }
    }

    bool IsPlayerCollision(Collision collision) //Fonction de detection si c'est le joueur qui collisionne
    {
        return (playerLayer.value & 1 << collision.gameObject.layer) > 0;
    }

    bool IsCollisionFromBottom(Collision collision) //Fonction de detection si la collision vient du dessous
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