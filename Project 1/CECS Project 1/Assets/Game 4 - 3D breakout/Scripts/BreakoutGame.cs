using UnityEngine;
using System.Collections;

public enum BreakoutGameState { playing, won, lost };

public class BreakoutGame : MonoBehaviour
{
    public static BreakoutGame SP;

    public Transform ballPrefab;

    private int totalBlocks;
    private int blocksHit;
    private BreakoutGameState gameState;
    private bool _paused = false;
    public bool paused
    {
        get
        {
            return _paused;
        }
        set
        {
            if (value != _paused)
            {
                Time.timeScale = value ? 0 : 1;
                _paused = value;
            }
        }
    }
    private float startTime;
    private const float maxTime = 30;


    void Awake()
    {
        SP = this;
        blocksHit = 0;
        gameState = BreakoutGameState.playing;
        totalBlocks = GameObject.FindGameObjectsWithTag("Pickup").Length;
        Time.timeScale = 1.0f;
        startTime = Time.time;
        SpawnBall();
    }

    void SpawnBall()
    {
        Instantiate(ballPrefab, new Vector3(1.81f, 1.0f , 9.75f), Quaternion.identity);
    }

    void OnGUI(){
    
        GUILayout.Space(10);
        GUILayout.Label("  Hit: " + blocksHit + "/" + totalBlocks);

        if (gameState == BreakoutGameState.lost)
        {
            GUILayout.Label("You Lost!");
            if (GUILayout.Button("Try again"))
            {
                Application.LoadLevel(Application.loadedLevel);
            }
        }
        else if (gameState == BreakoutGameState.won)
        {
            GUILayout.Label("You won!");
            if (GUILayout.Button("Play again"))
            {
                Application.LoadLevel(Application.loadedLevel);
            }
        }
        else
        {
            float curCount = maxTime - (Time.time - startTime),
                minutes = curCount / 60,
                seconds = curCount % 60,
                miliseconds = (curCount * 100) % 100;
            if (curCount <= 0)
            {
                gameState = BreakoutGameState.lost;
                paused = true;
            }
            string countText = string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, miliseconds);
            GUILayout.Label(countText);
            paused = GUILayout.Toggle(paused, "Pause");
        }
    }

    public void HitBlock()
    {
        blocksHit++;
        
        //For fun:
        if (blocksHit%10 == 0) //Every 10th block will spawn a new ball
        {
            SpawnBall();
        }

        
        if (blocksHit >= totalBlocks)
        {
            WonGame();
        }
    }

    public void WonGame()
    {
        Time.timeScale = 0.0f; //Pause game
        gameState = BreakoutGameState.won;
    }

    public void LostBall()
    {
        int ballsLeft = GameObject.FindGameObjectsWithTag("Player").Length;
        if(ballsLeft<=1){
            //Was the last ball..
            SetGameOver();
        }
    }

    public void SetGameOver()
    {
        Time.timeScale = 0.0f; //Pause game
        gameState = BreakoutGameState.lost;
    }
}
