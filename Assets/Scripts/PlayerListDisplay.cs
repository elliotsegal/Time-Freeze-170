using UnityEngine;

public class PlayerListDisplay : MonoBehaviour
{
    public static PlayerListDisplay singleton { get; private set; }

    public PlayerDisplay itemPrefab;

    private void Awake()
    {
        singleton = this;
    }

    public void AddPlayer(PlayerController player)
    {
        PlayerDisplay display = Instantiate(itemPrefab);
        display.player = player;
        display.transform.SetParent(transform, false);
    }
}
