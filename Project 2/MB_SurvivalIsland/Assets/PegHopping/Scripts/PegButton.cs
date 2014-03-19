using UnityEngine;
using System.Collections;

public class PegButton : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnCollisionEnter(Collision info)
	{
		Debug.Log ("coll " + info.gameObject.name);
		
		if(info.gameObject.tag == "Player")
		{
			gameObject.animation.Play();
			PegMinigame.PM.PegButtonPressed();
		}
		else
		{
			
		}
	}
}
