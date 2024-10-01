using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpsVehicleControl : MonoBehaviour
{
    [SerializeField] float OpsVehicleSpeed = 25f;
    void FixedUpdate()
    {
        transform.Translate(Vector3.forward * 0.5f * OpsVehicleSpeed * Time.fixedDeltaTime);
    }
}
