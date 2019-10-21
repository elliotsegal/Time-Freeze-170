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

    private float timer;
    private TextMesh textMesh;

    private void Start()
    {
        timer = startTime;
        textMesh = GetComponent<TextMesh>();
        textMesh.color = GetComponentInParent<PlayerController>().color;
    }

    private void Update()
    {
        if (state == TimerState.Normal)
        {
            timer -= Time.deltaTime;
        }
        else if (state == TimerState.Accelerated)
        {
            timer -= Time.deltaTime * 2;
        }

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
}