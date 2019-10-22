﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public interface IPlayerList
{
    void AddPlayer(PlayerInput player);
    void RemovePlayer(PlayerInput player);
}

public class PlayerManager : MonoBehaviour
{
    private static int playerID = 0;
    public static PlayerManager singleton { get; private set; }
    private bool gameStarted = false;
    private bool gameEnded = false;

    public GameObject[] playerListObjects;
    public GameObject[] spawnPoints;
    [Space]
    public UnityEvent onGameStarted = new UnityEvent();
    public UnityEvent onGameStopped = new UnityEvent();


    private IPlayerList[] playerLists;

    private List<PlayerController> players;
    private List<GameObject> unusedSpawnPoints;
    private bool playing;

    private void Start()
    {
        singleton = this;
        playerLists = new IPlayerList[playerListObjects.Length];
        for (int i = 0; i < playerListObjects.Length; ++i)
        {
            playerLists[i] = playerListObjects[i].GetComponent<IPlayerList>();
        }

        players = new List<PlayerController>();
        unusedSpawnPoints = new List<GameObject>(spawnPoints);
        playing = false;
    }

    public void OnPlayerJoined(PlayerInput playerInput)
    {
        playerInput.name = "Player " + (++playerID);
        PlayerController player = playerInput.GetComponentInChildren<PlayerController>();
        player.Init();
        player.color = GetColorForIndex(playerID);
        player.transform.position = GetNextSpawnPosition();
        player.SetFrozen(true, player);
        players.Add(player);
        foreach (IPlayerList list in playerLists)
        {
            list.AddPlayer(playerInput);
        }
    }

    public void OnPlayerLeft(PlayerInput playerInput)
    {
        players.Remove(playerInput.GetComponentInChildren<PlayerController>());
        foreach (IPlayerList list in playerLists)
        {
            list.RemovePlayer(playerInput);
        }
    }

    public void StartGame()
    {
        playing = true;
        onGameStarted.Invoke();
        int id = 0;
        foreach (PlayerController player in players)
        {
            player.SetFrozen(false, null);
            player.setID(id);
            id++;
        }
        gameStarted = true;

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

    private Vector2 GetNextSpawnPosition()
    {
        if (unusedSpawnPoints.Count == 0)
        {
            throw new System.Exception("Not enough spawn positions");
        }
        GameObject spawnPoint = unusedSpawnPoints[Random.Range(0, unusedSpawnPoints.Count)];
        unusedSpawnPoints.Remove(spawnPoint);
        return spawnPoint.transform.position;
    }

    public IEnumerable<PlayerController> GetOtherPlayers(PlayerController player)
    {
        foreach (PlayerController p in players)
        {
            if (p == player) continue;
            yield return p;
        }
    }
    private void FixedUpdate()
    {
        if (gameStarted && !gameEnded)
        { 
            int playersLeft = 0;
            int lastPlayer = -1;
            foreach (PlayerController player in players)
            {
                if (player.getTime() > 0)
                {
                    lastPlayer = player.getID();
                    playersLeft++;
                }
            }
            if (playersLeft == 1)
            {
                gameEnded = true;
                endRound(lastPlayer);
            }
            else if (playersLeft == 0)
            {
                gameEnded = true;
                endRound(-1);
            }
        }
    }
    private void endRound(int playerID)
    {
        GameObject roundEndTextObject = GameObject.Find("RoundEndText");
        UnityEngine.UI.Text roundEndText = roundEndTextObject.GetComponent<UnityEngine.UI.Text>();
        if (playerID == -1)
        {
            roundEndText.text = "No Winner";
        }
        else
        {
            roundEndText.text = "Player " + (1 + playerID).ToString() + " Wins";
        }
    }
}
