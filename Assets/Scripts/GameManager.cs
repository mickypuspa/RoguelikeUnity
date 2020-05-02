using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public float turnDeley = 0.1f;

    public BoardManager boardScript;
    public int playerFoodPoints = 100;
    [HideInInspector]public bool playerTurn = true;

    private List<Enemy> enemies = new List<Enemy>();
    private bool enemiesMoving;

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
        enemies.Clear();
        boardScript.SetupScene(3);
    }

    public void GameOver()
    {
        enabled = false;

    }

    IEnumerator MoveEnemies()
    {
        enemiesMoving = true;
        yield return new WaitForSeconds(turnDeley);
        if(enemies.Count == 0)
        {
            yield return new WaitForSeconds(turnDeley);
        }

        for (int i = 0; i < enemies.Count; i++)
        {
            enemies[i].MoveEnemy();
            yield return new WaitForSeconds(enemies[i].moveTime);
        }

        playerTurn = true;
        enemiesMoving = false;
    }

    private void Update()
    {
        if (playerTurn || enemiesMoving) return;

        StartCoroutine(MoveEnemies());
    }

    public void AddEnemyToList(Enemy enemy)
    {
        enemies.Add(enemy);
    }


}
