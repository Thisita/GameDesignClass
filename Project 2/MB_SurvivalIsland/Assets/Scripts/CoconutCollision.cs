using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class CoconutCollision : MonoBehaviour {
	
	public GameObject targetRoot;
	public AudioClip hitSound;
	public AudioClip resetSound;
	
	private bool beenHit = false;
	private float timer = 0;
	
	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(beenHit)
		{
			timer += Time.deltaTime;
		}
		
		if(timer > 3)
		{
			audio.PlayOneShot(resetSound);
			targetRoot.animation.Play("up");
			CoconutWin.targets--;
			beenHit = false;
			timer = 0;
		}
	}
	
	void OnCollisionEnter(Collision theObject)
	{
		if(!beenHit && theObject.gameObject.name == "coconut")
		{
			audio.PlayOneShot(hitSound);
			targetRoot.animation.Play("down");
			CoconutWin.targets++;
			beenHit = true;
		}
	}
	
	
}
