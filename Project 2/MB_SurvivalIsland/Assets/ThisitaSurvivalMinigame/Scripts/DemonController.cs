using UnityEngine;
using System.Collections;

public class DemonController : MonoBehaviour {

    private GameObject player;
    private CharacterController controller;
    public float speed;
    public float dieRange;

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

        GameObject.Find("hinttext").GetComponent<HintText>().SetHint("RUN AWAY! THE DEMON SPIRIT HAS AWOKEN!");

        float range = Vector3.Distance(player.transform.position, transform.position);

        if (range <= dieRange)
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
}
