using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameManager gameManager;

    public int health = 1;
    public bool isHit = false;

    public float speed;
    //point A à B
    public Transform[] waypoints;
    // change de direction après attain A OU B
    private Transform target;
    private int destPoint = 0;

    //de lorenzo code
    public LayerMask playerLayer;

    // Start is called before the first frame update
    void Start()
    {
        target = waypoints[0];
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized*speed*Time.deltaTime, Space.World);

        // calculer la target = but de déplacement
        //si distance inférieur à .. alors changement de but 
        if(Vector3.Distance(transform.position, target.position) < 0.3f)
        {
            //destpoint = but de direction
            //target = l'objet
            //waypoint A ou B = destpoint 0 ou 1
            //! risque de perte de but après tout les but atteint au moin une fois 
    
            destPoint = (destPoint + 1) % waypoints.Length;
            target = waypoints[destPoint];

        }

        //Si la vie = 0, on detruit l'enemie et on ajoute le score
        if(health == 0)
        {
            Destroy(gameObject);
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
