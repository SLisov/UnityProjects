using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOutBounds : MonoBehaviour
{
    public float topBound = 35f;
    public float lowerbound = -14f;
    public float sidesBound = 27;
    void Start() 
    {
        
    }

    
    void Update()
    {
        if (transform.position.z > topBound)
        {
            Destroy(gameObject);
        }
        else if (transform.position.z < lowerbound)
        {
            Destroy(gameObject);

        }
        else if (transform.position.x > sidesBound)
        {
            Destroy(gameObject);
        }
        else if (transform.position.x < -sidesBound)
        {
            Destroy(gameObject);
        }

    }
}
