using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Delivery : MonoBehaviour
{
    bool hasPackage;
    [SerializeField] Color32 hasPackageColor = new Color32(238, 160, 44, 255);
    [SerializeField] Color32 hasNoPackageColor = new Color32(255,255,255,255);
    [SerializeField] float timeBeforeDestroy = 0.1f;

    SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        //Debug.Log("What a bumpper!");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Package" && !hasPackage)
        {
            Debug.Log("Package picked up");
            hasPackage = true;
            spriteRenderer.color = hasPackageColor;
            Destroy(other.gameObject, timeBeforeDestroy);
            
            
        }
        else if(other.tag == "Customer" && hasPackage)
        {
            Debug.Log("Package delivered");
            hasPackage = false;
            spriteRenderer.color = hasNoPackageColor;

        }
    }
}
