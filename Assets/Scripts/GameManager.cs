using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public BoardManager boardScript;
    public int playerFoodPoints = 100;
    [HideInInspector]public bool playerTurn = true;

    private void Awake()
    {
        if(GameManager.instance == null)
        {
            GameManager.instance = this;
        }else if (GameManager.instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

        boardScript = GetComponent<BoardManager>();
    }

    private void Start()
    {
        InitGame();
    }

    void InitGame()
    {
        boardScript.SetupScene(3);
    }

    public void GameOver()
    {
        enabled = false;

    }


}
