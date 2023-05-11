using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    //point of interest - what the camera is going to follow
    public GameObject poi;

    //initial z of the camera
    public float camZ;

    //variable for easing
    public float easing = 0.05f;

    //a follow cam singleton
    static public FollowCam Instance;
    public Vector2 minXY;

    private void Awake()
    {
        Instance = this;
        camZ = this.transform.position.z;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //only move when we have something to follow
        //if (poi == null) return;

        //get position of poi
        //Vector3 destination = poi.transform.position;
        Vector3 destination;

        if(poi == null)
        {
            destination = Vector3.zero;
        }
        else
        {
            destination = poi.transform.position;
            //if poi is a projectile, check to see if it's at rest
            if(poi.tag == "projectile")
            {
                //if poi is sleeping (sleeping is physics means not moving)
                Invoke("Wait", 2);
            }

            //limits the x and y values
            destination.x = Mathf.Max(minXY.x, destination.x);
            destination.y = Mathf.Max(minXY.y, destination.y);

        }

        //retain the destination.z of camZ
        destination.z = camZ;

        //move the camera to the destination
        transform.position = destination;
    }
}
