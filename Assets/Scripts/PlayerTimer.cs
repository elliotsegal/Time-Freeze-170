using UnityEngine;

public class PlayerTimer : MonoBehaviour
{
    private float timer;
    private TextMesh textMesh;

    private void Start()
    {
        timer = 5;
        textMesh = GetComponent<TextMesh>();
        textMesh.color = GetComponentInParent<PlayerController>().color;
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            textMesh.text = "=(";
        }
        else
        {
            textMesh.text = timer.ToString("N2");
        }
    }
}