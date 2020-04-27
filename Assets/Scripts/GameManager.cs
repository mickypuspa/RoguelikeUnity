﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public BoardManager boardScript;

    private void Awake()
    {
        boardScript = GetComponent<BoardManager>();
    }

    private void Start()
    {
        boardScript.SetupScene();
    }

    void InitGame()
    {
        boardScript.SetupScene();
    }


}
