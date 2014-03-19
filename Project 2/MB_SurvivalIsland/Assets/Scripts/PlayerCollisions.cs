using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(AudioSource))]
public class PlayerCollisions : MonoBehaviour
{	
	private bool doorIsOpen = false;
	private float doorTimer = 0.0f;
	private GameObject currentDoor;
	
	private bool haveMatches = false;
	private bool fireLit = false;
	
	public float doorOpenTime = 3.0f;
	public AudioClip doorOpenSound;
	public AudioClip doorShutSound;
	public AudioClip batteryCollect;
	public GameObject matchGUI;
	
	private bool gaveCoconutHint = false;
	private bool offButton = true;
	
	// Use this for initialization
	void Start ()
	{
	}
	
	// Update is called once per frame
	void Update ()
	{	
		RaycastHit hit;
		
		if(Physics.Raycast(transform.position, transform.forward, out hit, 5))
		{
			if(hit.collider.gameObject.tag == "outpostDoor" && !doorIsOpen)
			{
				if(BatteryCollect.charge >= 4)
				{
					currentDoor = hit.collider.gameObject;
					Door(doorOpenSound, true, "dooropen", currentDoor);
					GameObject.Find("BatteryGUI").GetComponent<GUITexture>().enabled = false;
				}
				else
				{
					GameObject.Find("hinttext").GetComponent<HintText>().SetHint("The door seems to need more power...");
					GameObject.Find("BatteryGUI").GetComponent<GUITexture>().enabled = true;
				}
			}
		}
		
		if(doorIsOpen)
		{
			doorTimer += Time.deltaTime;
			
			if(doorTimer > doorOpenTime)
			{
				Door(doorShutSound, false, "doorshut", currentDoor);
				doorTimer = 0.0f;
			}
		}
		
	}
	
	void lightFire()
	{
		var campfire = GameObject.Find("campfire");
		var campSound = campfire.GetComponent<AudioSource>();
		campSound.Play();
		
		var flames = GameObject.Find("FireSystem");
		flames.GetComponent<ParticleEmitter>().emit = true;
		
		var smoke = GameObject.Find("SmokeSystem");
		smoke.GetComponent<ParticleEmitter>().emit = true;
		
		Destroy(GameObject.Find("matchGUI"));
		fireLit = true;
	}
	
	void OnControllerColliderHit(ControllerColliderHit hit)
	{
		bool onMat = (hit.collider == GameObject.Find("mat").collider);
		var crosshair = GameObject.Find("Crosshair").GetComponent<GUITexture>();
		
		if(onMat && !gaveCoconutHint)
		{
			gaveCoconutHint = true;
			var hints = GameObject.Find("hinttext").GetComponent<HintText>();
			hints.SetHint("Hit the targets and win a battery!");
		}
		
		if(hit.collider.gameObject == GameObject.Find("campfire"))
		{
			var hints = GameObject.Find("hinttext").GetComponent<HintText>();
			
			
			
			if(haveMatches)
			{
				haveMatches = false;
				lightFire();
				hints.SetHint("You lit the fire, you'll survive, well done!");
				
				StartCoroutine(LoadMenu());
			}
			else if(!fireLit)
			{
				hints.SetHint("I'll need some matches to light this camp fire...");
			}
		}
		
		// peg button game collider
		if(hit.collider.gameObject == GameObject.Find ("cylinder2"))
		{
			if(offButton)
			{
				Debug.Log ("Hit button " + Time.time);
				// ugly hack, but the button itself doesn't seem to collide right
				// so we do the collision triggering here
				hit.collider.gameObject.transform.parent.gameObject.animation.Play();
				PegMinigame.PM.PegButtonPressed();
			}
			
			offButton = false;
		}
		else
		{
			offButton = true;
		}
		
		CoconutThrow.canThrow = onMat;
		crosshair.enabled = onMat;
	}
	
	IEnumerator LoadMenu()
	{
		yield return new WaitForSeconds(5f);
		Application.LoadLevel("MenuScene");
	}
	
	
	void Door(AudioClip aClip, bool openCheck, string animName, GameObject thisDoor)
	{
		
		audio.PlayOneShot(aClip);
		doorIsOpen = openCheck;
		thisDoor.transform.parent.animation.Play(animName);
	}
	
	void OnTriggerEnter(Collider collisionInfo)
	{
		if(collisionInfo.gameObject.tag == "battery")
		{
			BatteryCollect.charge++;
			audio.PlayOneShot(batteryCollect);
			Destroy(collisionInfo.gameObject);
		}
		else if(collisionInfo.gameObject.name == "matchbox")
		{
			Destroy(collisionInfo.gameObject);
			haveMatches = true;
			audio.PlayOneShot(batteryCollect);
			var matchGUIobj = Instantiate(matchGUI, new Vector3(0.15f, 0.1f, 0f), transform.rotation);
			matchGUIobj.name = "matchGUI";
		}
	}
}
