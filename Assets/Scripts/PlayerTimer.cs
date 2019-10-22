using UnityEngine;

public class PlayerTimer : MonoBehaviour
{
    public float startTime;

    public bool hasTime => timer > 0;

    private PlayerController player;
    private float timer;
    private TextMesh textMesh;

    private void Start()
    {
        timer = startTime;
        player = GetComponentInParent<PlayerController>();
        textMesh = GetComponent<TextMesh>();
        textMesh.color = player.color;
    }

    private void Update()
    {
        timer -= Time.deltaTime * player.timeMultiplier;
      
        if (timer < 0)
        {
            textMesh.text = "";
        }
        else
        {
            textMesh.text = timer.ToString("N2");
            textMesh.fontStyle = player.timeMultiplier > 1 ? FontStyle.Bold : FontStyle.Normal;
        }
    }

    public void AddTime(float time)
    {
        timer += time;
    }
}