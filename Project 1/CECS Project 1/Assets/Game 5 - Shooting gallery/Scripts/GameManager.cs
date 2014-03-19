using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public static GameManager SP;
    public static GameState gameState;

    private ArrayList objectsList;
    private float moveSpeed = 1.5f;
    private int spawnedObjects = 0;
    private int score;
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

	void Awake () {
        SP = this;

        objectsList = new ArrayList();
        spawnedObjects = score = 0;
        gameState = GameState.playing;
        startTime = Time.time;
        paused = true;
        paused = false;
	}

    void Update()
    {
        //Move objects
        for (int i = objectsList.Count - 1; i >= 0; i--)
        {
            float farLeft = -10;
            float farRight = 10;

            MovingObject movObj = (MovingObject)objectsList[i];
            Transform trans = movObj.transform;
            trans.Translate((int)movObj.direction * Time.deltaTime * moveSpeed, 0, 0);
            if (trans.position.x < farLeft || trans.position.x > farRight)
            {
                Destroy(trans.gameObject);
                objectsList.Remove(movObj);
            }
        }
    }

    void OnGUI(){
        if(GUILayout.Button("Restart")){
            paused = false;
            Application.LoadLevel(Application.loadedLevel);
        }
        if(gameState == GameState.gameover)
        {
            GUILayout.Label(" Hit: " + score + "/" + spawnedObjects);
            GUILayout.Label("Game over");
            paused = true;
        }
        else
        {
            float curCount = maxTime - (Time.time - startTime),
                minutes = curCount / 60,
                seconds = curCount % 60,
                miliseconds = (curCount * 100) % 100;
            if (curCount <= 0) gameState = GameState.gameover;
            string countText = string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, miliseconds);
            GUILayout.Label(countText);
            GUILayout.Label(" Hit: " + score + "/" + spawnedObjects);
            paused = GUILayout.Toggle(paused, "Pause");
        }
    }

    public void AddTarget(MovingObject newObj){
        spawnedObjects++;
        objectsList.Add(newObj);
    }

    public bool RemoveObject(Transform trans)
    {
        
        foreach (MovingObject obj in objectsList)
        {
            if (obj.transform == trans)
            {
                score++;
                objectsList.Remove(obj);
                Destroy(obj.transform.gameObject); 
                return true; 
            }
        }
        Debug.LogError("ERROR: Couldn't find target!");
        return false;
    }

}
