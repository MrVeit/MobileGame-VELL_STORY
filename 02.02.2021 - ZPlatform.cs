using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZPlatform : MonoBehaviour
{
    float dirZ;
    float speed = 2f;
    bool MovingBack = true;

    void Update()
    {
        if (transform.position.z > 44.42f)
        {
            MovingBack = false;
        }
        else if (transform.position.z < 24f)
        {
            MovingBack = true;
        }
        if (MovingBack)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + speed * Time.deltaTime);
        }
        else
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - speed * Time.deltaTime);
        }
    }
}

