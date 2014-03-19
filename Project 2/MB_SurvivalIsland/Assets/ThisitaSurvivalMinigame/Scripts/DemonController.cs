using UnityEngine;
using System.Collections;

public class DemonController : MonoBehaviour {

    private GameObject player;
    private CharacterController controller;
    public float speed;
    public float dieRange;
    public float dieDelay;

    private float chaseStartTime;

    private bool chasing = false;
    public bool Chasing
    {
        get
        {
            return chasing;
        }
        set
        {
            if (value && !chasing)
            {
                chaseStartTime = Time.time;
                chasing = value;
            }
        }
    }

	// Use this for initialization
	void Start () {
        controller = GetComponent<CharacterController>();
        player = GameObject.Find("First Person Controller");
	}
	
	// Update is called once per frame
	void Update () {
        if (player == null || !Chasing) return;

        float range = Vector3.Distance(player.transform.position, transform.position);

        if (range <= dieRange && (Time.time - chaseStartTime) >= dieDelay)
        {
            // Player loses and is cursed
            Application.LoadLevel(Application.loadedLevel);
        }
        else
        {
            // Chase the player
            transform.LookAt(player.transform);
            Vector3 moveDirection = transform.TransformDirection(Vector3.forward);
            controller.Move(moveDirection * speed * Time.deltaTime);
        }
	}

    void OnGUI()
    {
        if (Chasing)
        {
            GUILayout.BeginVertical();
            GUILayout.FlexibleSpace();
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            GUILayout.Label("RUN FOR YOUR LIVES THE DEMON SPIRIT COMETH!!!");
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            GUILayout.FlexibleSpace();
            GUILayout.EndVertical();
        }
    }
}
