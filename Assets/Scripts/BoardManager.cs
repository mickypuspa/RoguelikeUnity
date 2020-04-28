using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BoardManager : MonoBehaviour
{
    public int columns = 8;
    public int rows = 8;


    public GameObject[] floorTiles, outerWallTiles, wallTiles, foodTiles, enemyTiles; //Losas diferentes
    public GameObject exit;

    private Transform boardHolder;
    private Transform objectHolder;

    //Vector para tener posiciones para poner objectos
    private List<Vector2> gridPositions = new List<Vector2>();

    void InitializeList()
    {
        gridPositions.Clear();

        for(int x = 1; x < columns - 1; x++)
        {
            for (int y = 1; y < rows - 1; y++)
            {
                gridPositions.Add(new Vector2(x, y));
            }
        }
    }

    //Obtener posiciones random de las disponibles
    Vector2 RandomPosition()
    {
        int randomIndex = Random.Range(0, gridPositions.Count);
        Vector2 randomPosition = gridPositions[randomIndex];
        gridPositions.RemoveAt(randomIndex);

        return randomPosition;
    }

    //Poner num random de objectos en posicion random
    void LayoutObjectAtRandom(GameObject[] tileArray, int min, int max)
    {
        int objectCount = Random.Range(min, max + 1);
        for(int i = 0; i < objectCount; i++)
        { 
            Vector2 randomPosition = RandomPosition();
            GameObject tileChoice = GetRandomInArray(tileArray);
            GameObject instance = Instantiate(tileChoice, randomPosition, Quaternion.identity);
            instance.transform.SetParent(objectHolder);
        }
    }
    public void SetupScene(int level) 
    {
        BoardSetup();
        InitializeList();
        objectHolder = new GameObject("Objects").transform;
        LayoutObjectAtRandom(wallTiles, 5, 9);
        LayoutObjectAtRandom(foodTiles, 1, 5);
        int enemyCount = (int)Mathf.Log(level, 2);
        LayoutObjectAtRandom(enemyTiles, enemyCount, enemyCount);
        Instantiate(exit, new Vector2(columns - 1, rows - 1), Quaternion.identity);
    }

    //Montar el tablero
    void BoardSetup()
    {
        boardHolder= new GameObject("Board").transform;

        for (int x = -1; x < columns+1; x++)
        {
            for (int y = -1; y < rows + 1; y++)
            {
                GameObject toIstantiate;

                if (x == -1 || y == -1 || x == columns || y == rows)
                {
                    toIstantiate = GetRandomInArray(outerWallTiles);
                }
                else
                {
                    toIstantiate = GetRandomInArray(floorTiles);
                }

                GameObject instance = Instantiate(toIstantiate, new Vector2(x, y), Quaternion.identity);
                instance.transform.SetParent(boardHolder);
            }
        }
    }

    GameObject GetRandomInArray(GameObject[] array)
    {
        return array[Random.Range(0, array.Length)];
    }
}
