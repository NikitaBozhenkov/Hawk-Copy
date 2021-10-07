using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Characters;
using Chunks;
using Configuration;
using TMPro;
using UnityEngine;

public class Meta : MonoBehaviour
{
    [SerializeField] private GameConfig gameConfig;
    private Game game;

    [Header("Main Menu Settings")] 
    [SerializeField] private Canvas mainMenuCanvas;

    private Action<GameStatus> gameStatusChanged;

    private void Start()
    {
        game = new GameObject("game").AddComponent<Game>();
        game.Setup(gameConfig);
        game.StartGame();
    }

    private void FinishGame()
    {
        mainMenuCanvas.gameObject.SetActive(true);
    }
}