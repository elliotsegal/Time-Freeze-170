using UnityEngine;

public enum TimerState
{
    Frozen,
    Normal,
    Accelerated
}

public class PlayerTimer : MonoBehaviour
{
    public float startTime;
    public TimerState state;

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
            textMesh.text = "=(";
            textMesh.fontStyle = FontStyle.Normal;
        }
        else
        {
            textMesh.text = timer.ToString("N2");
            textMesh.fontStyle = state == TimerState.Accelerated ? FontStyle.Bold : FontStyle.Normal;
        }
    }

    public void AddTime(float time)
    {
        timer += time;
    }
}