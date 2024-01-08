using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{

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

    }

    void OnCollisionEnter(Collision collision)
    {
         if (IsPlayerCollision(collision))
        {
            if(IsCollisionFromTop(collision))
            {
                Destroy(gameObject);

            }

            else if (IsCollisionFromRight(collision)|| IsCollisionFromLeft(collision))
            {
                //rétraicir mario ou mourir

            }






        }




    }

    bool IsPlayerCollision(Collision collision)
    {
        return (playerLayer.value & 1 << collision.gameObject.layer) > 0;
    }

    bool IsCollisionFromTop(Collision collision)
    {
        foreach (ContactPoint contact in collision.contacts)
        {
            if (contact.normal.y < 0.8f)
            {
                return true;
            }
        }
        return false;

        
    }
    bool IsCollisionFromRight(Collision collision)
    {
        //!! vérifier <
        foreach (ContactPoint contact in collision.contacts)
        {
            if (contact.normal.x < 0.8f)
            {
                return true;
            }
        }
        return false;
    }
    bool IsCollisionFromLeft(Collision collision)
    
    {
        foreach (ContactPoint contact in collision.contacts)
        {
            if (contact.normal.x > 0.8f)
            {
                return true;
            }
        }
        return false;

    }
}
