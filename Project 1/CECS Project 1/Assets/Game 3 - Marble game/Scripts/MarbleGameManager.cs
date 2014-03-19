using UnityEngine;
using System.Collections;

public enum MarbleGameState {playing, won,lost };

public class MarbleGameManager : MonoBehaviour
{
    public static MarbleGameManager SP;

    private int totalGems;
    private int foundGems;
    private MarbleGameState gameState;
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
    private const float maxTime = 300;


    void Awake()
    {
        SP = this; 
        foundGems = 0;
        gameState = MarbleGameState.playing;
        totalGems = GameObject.FindGameObjectsWithTag("Pickup").Length;
        startTime = Time.time;
        Time.timeScale = 1.0f;
    }

	void OnGUI () {
	    GUILayout.Label(" Found gems: "+foundGems+"/"+totalGems );
        

        if (gameState == MarbleGameState.lost)
        {
            GUILayout.Label("You Lost!");
            if(GUILayout.Button("Try again") ){
                Application.LoadLevel(Application.loadedLevel);
            }
        }
        else if (gameState == MarbleGameState.won)
        {
            GUILayout.Label("You won!");
            if(GUILayout.Button("Play again") ){
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
                gameState = MarbleGameState.lost;
                paused = true;
            }
            string countText = string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, miliseconds);
            GUILayout.Label(countText);
            paused = GUILayout.Toggle(paused, "Pause");
        }
	}

    public void FoundGem()
    {
        foundGems++;
        if (foundGems >= totalGems)
        {
            WonGame();
        }
    }

    public void WonGame()
    {
        Time.timeScale = 0.0f; //Pause game
        gameState = MarbleGameState.won;
    }

    public void SetGameOver()
    {
        Time.timeScale = 0.0f; //Pause game
        gameState = MarbleGameState.lost;
    }
}
