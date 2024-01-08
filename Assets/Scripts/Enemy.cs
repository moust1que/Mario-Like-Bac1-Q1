using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public float speed;
    //point A à B
    public Transform[] waypoints;
    // change de direction après attain A OU B
    private Transform target;
    private int destPoint = 0;

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
}
