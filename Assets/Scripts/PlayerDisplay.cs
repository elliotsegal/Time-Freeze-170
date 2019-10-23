using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerDisplay : MonoBehaviour
{
    [NonSerialized] public PlayerInput player;

    public static string GetPlayerName(PlayerController player)
    {
        string color = ColorUtility.ToHtmlStringRGB(player.color);
        return $"<color=#{color}>{player.transform.parent.name}</color>";
    }

    private void Start()
    {
        PlayerController controller = player.GetComponentInChildren<PlayerController>();
        GetComponent<Text>().text = GetPlayerName(controller) + "  " + player.devices[0].displayName;
    }

    private void Update()
    {
        if (player == null)
        {
            Destroy(gameObject);
        }
    }
}
