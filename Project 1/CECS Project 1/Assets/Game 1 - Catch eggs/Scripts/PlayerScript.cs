using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {
    public bool playing = true;
    private bool _paused = false;
    public bool paused
    {
        get
        {
            return _paused;
        }
        set
        {
            if(value != _paused)
            {
                Time.timeScale = value ? 0 : 1;
                _paused = value;
            }
        }
    }
    private float startTime;
    private const float maxTime = 90;
    public int theScore = 0;

    void Awake()
    {
        playing = true;
        paused = false;
        startTime = Time.time;
        paused = true;
        paused = false;
    }

	void Update () {
        //These two lines are all there is to the actual movement..
        float moveInput = Input.GetAxis("Horizontal") * Time.deltaTime * 3; 
        transform.position += new Vector3(moveInput, 0, 0);

        //Restrict movement between two values
        if (transform.position.x <= -2.5f || transform.position.x >= 2.5f)
        {
            float xPos = Mathf.Clamp(transform.position.x, -2.5f, 2.5f); //Clamp between min -2.5 and max 2.5
            transform.position = new Vector3(xPos, transform.position.y, transform.position.z);
        }
	}

    //OnGUI is called multiple times per frame. Use this for GUI stuff only!
    void OnGUI()
    {
        //We display the game GUI from the playerscript
        //It would be nicer to have a seperate script dedicated to the GUI though...
        GUILayout.Label("Score: " + theScore);
        if(playing)
        {
            float curCount = maxTime - (Time.time - startTime),
                minutes = curCount / 60,
                seconds = curCount % 60,
                miliseconds = (curCount * 100) % 100;
            if (curCount <= 0)
            {
                playing = false;
                paused = true;
            }
            string countText = string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, miliseconds);
            GUILayout.Label(countText);
            paused = GUILayout.Toggle(paused, "Pause");
        }
        else
        {
            if(GUILayout.Button("Restart"))
            {
                Application.LoadLevel(Application.loadedLevel);
            }
        }
    }
}
