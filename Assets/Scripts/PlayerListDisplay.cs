using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerListDisplay : MonoBehaviour, IPlayerList
{
    public PlayerDisplay itemPrefab;

    public void AddPlayer(PlayerInput player)
    {
        PlayerDisplay display = Instantiate(itemPrefab);
        display.player = player;
        display.transform.SetParent(transform, false);
    }

    public void RemovePlayer(PlayerInput player)
    {
        // Don't need to do anything here, individual PlayerDisplays
        // will destroy themselves when their player disappears.
    }
}
