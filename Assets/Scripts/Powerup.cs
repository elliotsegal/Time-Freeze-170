using UnityEngine;

public class Powerup : MonoBehaviour
{
    public float rotateSpeed;

    private void Update()
    {
        transform.GetChild(0).Rotate(0, rotateSpeed * Time.deltaTime, 0);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.layer == PlayerController.Layer)
        {
            collider.GetComponent<PlayerController>().OnPickUp(this);
            Destroy(gameObject);
        }
    }
}
