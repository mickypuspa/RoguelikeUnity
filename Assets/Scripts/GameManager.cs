using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public float turnDeley = 0.1f;
    public float levelStartDelay = 2f;
    public bool doingSetup;

    public BoardManager boardScript;
    public int playerFoodPoints = 100;
    [HideInInspector]public bool playerTurn = true;


    private List<Enemy> enemies = new List<Enemy>();
    private bool enemiesMoving;

    private int level = 0;
    private GameObject levelImage;
    private Text levelText;


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


    void InitGame()
    {
        doingSetup = true;

        levelImage = GameObject.Find("LevelImage");
        levelText = GameObject.Find("LevelText").GetComponent<Text>();
        levelText.text = "Day " + level;
        levelImage.SetActive(true);

        enemies.Clear();
        boardScript.SetupScene(level);

        Invoke("HideLevelImage", levelStartDelay);

    }

    private void HideLevelImage()
    {
        levelImage.SetActive(false);
        doingSetup = false;
    }

    public void GameOver()
    {
        levelText.text = "After " + level + " days, you starved.";
        levelImage.SetActive(true);
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
        if (playerTurn || enemiesMoving || doingSetup) return;

        StartCoroutine(MoveEnemies());
    }

    public void AddEnemyToList(Enemy enemy)
    {
        enemies.Add(enemy);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
        
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }

    private void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        level++;
        InitGame();

    }
}
