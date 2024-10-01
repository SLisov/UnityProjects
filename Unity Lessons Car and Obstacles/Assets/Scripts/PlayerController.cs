using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using System;

public class PlayerController : MonoBehaviour
{

    [SerializeField] float speed;
    [SerializeField] private float horsePower = 0;
    [SerializeField] GameObject centerOfMass;
    private const float turnSpeed = 50f;
    public GameObject cameraGameObject;
    float horizontalInput;
    float verticalInput;
    float rpm;
    private Rigidbody playerRb;
    private Vector3 offset = new Vector3(0, 3, 2);
    public GameObject playerInCabin;
    [SerializeField] TextMeshProUGUI speedometerText;
    [SerializeField] TextMeshProUGUI RPMText;
    [SerializeField] List<WheelCollider> allWheels;
    [SerializeField] int wheelsOnGround;
    

    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        //speedometerText = GetComponent<TextMeshProUGUI>();
        playerRb.centerOfMass = centerOfMass.transform.position;
    }


    void FixedUpdate()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        if (IsOnGround())
        {

        // MOVEMENT:

        // transform.Translate(Vector3.forward * verticalInput * speed * Time.fixedDeltaTime);
        playerRb.AddRelativeForce(Vector3.forward * horsePower * verticalInput);

        // ROTATION:

        transform.Rotate(Vector3.up * horizontalInput * turnSpeed * Time.fixedDeltaTime);
        cameraGameObject.transform.rotation = Quaternion.Euler(0, transform.eulerAngles.y, 2);
        cameraGameObject.transform.position = playerInCabin.transform.position + offset;

        speed = playerRb.velocity.magnitude * 3.6f;
        speedometerText.text = "Speed: " + Mathf.Round(speed) + " km/h";

        rpm = (speed % 30) * 40;
        RPMText.text = "RPM: " + Mathf.Round(rpm);

        }
    }

    bool IsOnGround() 
    { 
        wheelsOnGround = 0;

        foreach (WheelCollider wheel in allWheels)
        {
            if (wheel.isGrounded)
            {
                wheelsOnGround++;
            }
        }

        if (wheelsOnGround == 4) 
        { 
            return true;
        }
        else
        {
            return false;
        }
    }

}

