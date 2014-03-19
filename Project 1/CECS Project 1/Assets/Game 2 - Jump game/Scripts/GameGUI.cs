using UnityEngine;
using System.Collections;


public class GameGUI : MonoBehaviour {

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
    private const float maxTime = 90;

    public static GameGUI SP;
    public static int score;

    private int bestScore = 0;

    void Awake()
    {
        SP = this;
        score = 0;
        bestScore = PlayerPrefs.GetInt("BestScorePlatforms", 0);
        startTime = Time.time;
    }

    void OnGUI()
    {
        GUILayout.Space(3);
        GUILayout.Label(" Score: " + score);
        GUILayout.Label(" Highscore: " + bestScore);

        if (GameControl.gameState == GameState.gameover)
        {
            GUILayout.BeginArea(new Rect(0, 0, Screen.width, Screen.height));

            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            GUILayout.BeginVertical();
            GUILayout.FlexibleSpace();

            GUILayout.Label("Game over!");
            if (score > bestScore)
            {
                GUI.color = Color.red;
                GUILayout.Label("New highscore!");
                GUI.color = Color.white;
            }
            if (GUILayout.Button("Try again"))
            {
                Application.LoadLevel(Application.loadedLevel);
            }

            GUILayout.FlexibleSpace();
            GUILayout.EndVertical();
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            GUILayout.EndArea();

        }
        else
        {
            float curCount = maxTime - (Time.time - startTime),
                minutes = curCount / 60,
                seconds = curCount % 60,
                miliseconds = (curCount * 100) % 100;
            if (curCount <= 0)
            {
                GameControl.gameState = GameState.gameover;
                paused = true;
            }
            string countText = string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, miliseconds);
            GUILayout.Label(countText);
            paused = GUILayout.Toggle(paused, "Pause");
        }
    }

    public void CheckHighscore()
    {
        if (score > bestScore)
        {
            PlayerPrefs.SetInt("BestScorePlatforms", score);
        }
    }
}
