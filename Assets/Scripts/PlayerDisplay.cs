using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerDisplay : MonoBehaviour
{
    [NonSerialized] public PlayerController player;

    private void Start()
    {
        string color = ColorUtility.ToHtmlStringRGB(player.color);
        string inputDevice = player.GetComponent<PlayerInput>().devices[0].displayName;
        GetComponent<Text>().text = $"<color=#{color}>{player.name}</color>  {inputDevice}";
    }

    private void Update()
    {
        if (player == null)
        {
            Destroy(gameObject);
        }
    }
}
