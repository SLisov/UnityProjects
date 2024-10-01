using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedController : MonoBehaviour
{
    [SerializeField] float boostSpeed = 35f;
    [SerializeField] float normalSpeed = 20f;
    SurfaceEffector2D surfaceEffector;
    void Start()
    {   
        surfaceEffector = GetComponent<SurfaceEffector2D>();
    }

    void Update()
    {
        RotatePlayer();
    }

    void RotatePlayer()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            surfaceEffector.speed = boostSpeed;
        }

        else
        {
            surfaceEffector.speed = normalSpeed;
        }
    }
}
