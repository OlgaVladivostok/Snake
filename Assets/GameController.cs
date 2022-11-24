using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
   public enum GameState { MENU,GAME,GAMEOVER}
    public static GameState gameState;
    
    [Header("Managers")]
    public SnakeMovement SM;
    public BlocksManager BM;
    
    [Header("Canvas Group")]
    public CanvasGroup MENU_CG;
    public CanvasGroup GAME_CG;
    public CanvasGroup GAMEOVER_CG;

    [Header("ScoreManagement")]
    public Text ScoreText;
    public Text MenuScoreText;
    public Text BestScoreText;
    public static int SCORE;
    public static int BESTSCORE;

    [Header("SomeBool")]

    bool speedAdded;


    private void Start()
    {
        SetMenu();
        SCORE = 0;
        speedAdded = false;
        BESTSCORE = PlayerPrefs.GetInt("BESTSCORE");
    }

    private void Update()
    {
        ScoreText.text = SCORE + "";
        MenuScoreText.text = SCORE + "";
        if (SCORE>BESTSCORE)
        
            BESTSCORE = SCORE;
            BestScoreText.text = BESTSCORE + "";

            if(!speedAdded&&SCORE>150)
            {
                SM.speed++;
                speedAdded = true;
            }

        }
    public void SetMenu()
    {
        gameState = GameState.MENU;
        EnableCanvaGroup(MENU_CG);
        DisableCanvaGroup(GAME_CG);
        DisableCanvaGroup(GAMEOVER_CG);
    }

    public void SetGameOver()
    {
        gameState = GameState.GAMEOVER;
        DisableCanvaGroup(MENU_CG);
        DisableCanvaGroup(GAME_CG);
        EnableCanvaGroup(GAMEOVER_CG);

        foreach (GameObject g in GameObject.FindGameObjectsWithTag("Box"))
        {
            Destroy(g);
        }
        foreach (GameObject g in GameObject.FindGameObjectsWithTag("Bar"))
        {
            Destroy(g);
        }
        foreach (GameObject g in GameObject.FindGameObjectsWithTag("SimpleBox"))
        {
            Destroy(g);
        }
        foreach (GameObject g in GameObject.FindGameObjectsWithTag("Snake"))
        {
            Destroy(g);
        }
        SM.SpawnBodyPart();
        BM.SetPreviousSnakePosAfterGameOver();
        speedAdded = false;
        SM.speed = 3;
        PlayerPrefs.SetInt("BESTSCORE",BESTSCORE);
        BM.SimpleBoxPositions.Clear();

    }
    public void EnableCanvaGroup(CanvasGroup cg)
    {
        cg.alpha = 1;
        cg.blocksRaycasts = true;
        cg.interactable = true;
    }

    public void DisableCanvaGroup(CanvasGroup cg)
    {
        cg.alpha = 0;
        cg.blocksRaycasts = false;
        cg.interactable = false;
    }



























}
