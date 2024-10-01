using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ItemCollectable : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI m_TextMeshProUGUI;

    private int cherries;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Collectable"))
        {
            Destroy(collision.gameObject);
            cherries++;
            Debug.Log(cherries.ToString());
            m_TextMeshProUGUI.text = "Cherries : " + cherries.ToString();
        }
    }
}
