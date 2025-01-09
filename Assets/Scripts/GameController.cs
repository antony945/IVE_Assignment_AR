using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    TMP_Text scoreText;
    private int score;
    private int HIT_SCORE = 500;

    [SerializeField]
    TMP_Text finalScoreText;

    private int currentMiss;
    private int MAX_MISS = 1;

    [SerializeField]
    TMP_Text enemyCountText;
    private int enemyCount;

    [SerializeField]
    TimerController timerController;

    [SerializeField]
    UIController uiController;

    private string logEntry = string.Empty;

    // Static variable to store the Path
    public static string File_Path = string.Empty;

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        currentMiss = 0;
        enemyCount = 0;

        createEventLog();
    }

    void Update()
    {
        if (!timerController.isActiveAndEnabled)
            return;
        
        if (CheckWin())
        {
            logEntry = $"***** Win (GameController),{score},{DateTime.Now}***** \n";
            File.AppendAllText(GameController.File_Path, logEntry);
            finalScoreText.text = "Score: " + score.ToString();
            uiController.WinGame();
        }
        else if (CheckDefeat())
        {
            logEntry = $"***** Lose (GameController),{score},{DateTime.Now}***** \n";
            File.AppendAllText(GameController.File_Path, logEntry);
            uiController.LoseGame();
        }
    }

    public void UpdateUI()
    {
        scoreText.text = score.ToString();
        enemyCountText.text = enemyCount.ToString() + " more";
    }

    public void HandleHit()
    {
        // Increase score based on remaining time
        score += Mathf.FloorToInt(timerController.GetRemainingTime() / timerController.GetMaximumTime() * HIT_SCORE);

        // Decrease enemy count
        enemyCount--;

        logEntry = $"Hit (GameController),{score},{DateTime.Now} \n";
        File.AppendAllText(GameController.File_Path, logEntry);
    }

    public void HandleMiss()
    {
        // Increase current miss
        currentMiss++;

        logEntry = $"Miss (GameController),{score},{DateTime.Now} \n";
        File.AppendAllText(GameController.File_Path, logEntry);
    }

    public bool CheckWin()
    {
        // Hit all enemies
        return enemyCount <= 0;
    }

    public bool CheckDefeat()
    {
        return timerController.TimeIsExpired() || currentMiss >= MAX_MISS;
    }

    public void HandleEnemySpawn()
    {
        enemyCount++;
        UpdateUI();

        logEntry = $"NPC Spawn (GameController),Enemy,{DateTime.Now} \n";
        File.AppendAllText(GameController.File_Path, logEntry);
    }

    public void HandleAllySpawn()
    {
        logEntry = $"NPC Spawn (GameController),Ally,{DateTime.Now} \n";
        File.AppendAllText(GameController.File_Path, logEntry);
    }

    void createEventLog()
    {
        string projectPath = Directory.GetParent(Application.dataPath).FullName;
        string NameFile = String.Concat("EventsLog_", DateTime.Now.ToString("dd_MM_yyyy"), ".csv");
        File_Path = Path.Combine(projectPath, NameFile);

        // Si el archivo no existe, crea uno nuevo y pone un encabezado
        if (!File.Exists(File_Path))
        {
            File.WriteAllText(File_Path, " Game starts at: " + System.DateTime.Now.ToString() + "  \n");
            File.AppendAllText(File_Path, "Event,Result,Datetime\n");
        }
    }
}
