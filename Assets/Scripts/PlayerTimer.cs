﻿using UnityEngine;

public class PlayerTimer : MonoBehaviour
{
    public float startTime;

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
            textMesh.fontStyle = player.timeMultiplier > 1 ? FontStyle.Bold : FontStyle.Normal;
        }
    }
    public float getTime()
    {
        return timer;
    }
    public void AddTime(float time)
    {
        timer += time;
    }
}