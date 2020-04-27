using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BoardManager : MonoBehaviour
{
    public int columns = 8;
    public int rows = 8;

    public GameObject[] floorTiles, outerWallTiles;  //Losas del suelo

    private Transform boardHolder;

    public void SetupScene() {

        BoardSetup();
    }

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
