using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    private static int playerID = 0;

    public void OnPlayerJoined(PlayerInput playerInput)
    {
        PlayerController controller = playerInput.GetComponent<PlayerController>();
        controller.name = "Player " + (++playerID);
        controller.color = GetColorForIndex(playerID);
        PlayerListDisplay.singleton.AddPlayer(controller);
    }

    public void OnPlayerLeft(PlayerInput playerInput)
    {

    }

    private static Color GetColorForIndex(int index)
    {
        switch (index)
        {
            case 1: return Color.blue;
            case 2: return Color.red;
            case 3: return Color.green;
            case 4: return Color.yellow;
            case 5: return new Color(0.5f, 0, 1);
            case 6: return Color.magenta;
            case 7: return Color.cyan;
            case 8: return new Color(1, 0.5f, 0);
            default: throw new System.NotImplementedException();
        }
    }
}
