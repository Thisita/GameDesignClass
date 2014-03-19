using UnityEngine;
using System.Collections;

public class DemonController : MonoBehaviour {

    private GameObject player;
    private CharacterController controller;
    public float speed;
    public float dieRange;
    public float dieDelay;

    private float chaseStartTime;

    public float timer = 30.0f;
    private float mTimeRemaining;

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
                mTimeRemaining = timer;
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
            mTimeRemaining -= Time.deltaTime;

            // display countdown timer after three seconds from giving the go message
            if (mTimeRemaining < (timer - 3))
            {
                GameObject.Find("hinttext").GetComponent<HintText>()
                    .SetHint(mTimeRemaining.ToString("00.00"));
            }

            if (mTimeRemaining < 0)
            {
                // player has lost, reset everything
                GameObject.Find("hinttext").GetComponent<HintText>()
                    .SetHint("You outlasted the SPIRIT DEMON!");

                Destroy(gameObject);
            }


            // Chase the player
            transform.LookAt(player.transform);
            Vector3 moveDirection = transform.TransformDirection(Vector3.forward);
            controller.Move(moveDirection * speed * Time.deltaTime);
        }
	}
}
