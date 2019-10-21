using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerDisplay : MonoBehaviour
{
    [NonSerialized] public PlayerInput player;

    private void Start()
    {
        string color = ColorUtility.ToHtmlStringRGB(player.GetComponentInChildren<PlayerController>().color);
        string inputDevice = player.devices[0].displayName;
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
