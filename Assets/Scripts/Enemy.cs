using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameManager gameManager;

    public int health = 1;
    public bool isHit = false;

    //de lorenzo code
    public LayerMask playerLayer;

    // Update is called once per frame
    void Update()
    {
        //Si la vie = 0, on detruit l'enemie et on ajoute le score
        if(health == 0)
        {
			gameObject.SetActive(false);
            // Destroy(gameObject);
            gameManager.AddScore(500);
        }
        //Si l'enemie est touché par le joueur, on le fait mourir
        if (isHit)
        {
            isHit = false;
            gameManager.DeathPlayer();
        }

    }

    void OnCollisionEnter(Collision collision)
    {
         if (IsPlayerCollision(collision)) //Detecte si c'est le joueur qui le collisionne
        {
            if (CollisionSide(collision) == "Top") //Si la collision vient du haut
            {
                health = 0; //Definition de la vie à 0
            }
            else if(CollisionSide(collision) == "Side") //Si la collision vient du coté
            {
                isHit = true; //Definition de isHit à vrai
            }
        }
    }

    //Fonction de detection du joueur
    bool IsPlayerCollision(Collision collision)
    {
        return (playerLayer.value & 1 << collision.gameObject.layer) > 0;
    }

    //Fonction de detection de où vient la collision
    string CollisionSide(Collision collision)
    {
        if (collision.transform.position.y > transform.position.y)
        {
            return "Top";
        }
        else
        {
            return "Side";
        }
    }
}