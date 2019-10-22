using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.layer == PlayerController.Layer)
        {
            collider.GetComponent<PlayerController>().OnHurt();
        }
    }
}
