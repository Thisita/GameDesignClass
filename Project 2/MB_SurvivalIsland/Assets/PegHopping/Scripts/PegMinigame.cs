using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class PegMinigame : MonoBehaviour {
	
	public static PegMinigame PM;
	public GameObject battery;
	public AudioClip playSound;
	
	public float timer = 60.0f;
	private float mTimeRemaining;
	
	private GameObject mBattery;
	private Vector3 mBatteryLoc;
	
	private enum PMStates
	{
		Idle,
		Playing,
		Completed
	}; 
	
	private PMStates mState;
	
	// Use this for initialization
	void Start () {
		PM = this;
		mBatteryLoc = GameObject.Find ("HiddenBattery").transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		
		if(mState != PMStates.Playing)
			return;
		
		if(mBattery == null)
		{
			GameObject.Find("hinttext").GetComponent<HintText>()
				.SetHint("Congratulations!");
			
			mState = PMStates.Completed;
			return;
		}
		
		mTimeRemaining -= Time.deltaTime;
		
		
		// display countdown timer after three seconds from giving the go message
		if(mTimeRemaining < (timer - 3))
		{
			GameObject.Find("hinttext").GetComponent<HintText>()
				.SetHint(mTimeRemaining.ToString ("00.00"));
		}
		
		if(mTimeRemaining < 0)
		{
			// player has lost, reset everything
			GameObject.Find("hinttext").GetComponent<HintText>()
				.SetHint("You ran out of time to get the battery. Try Again.");
			
			Destroy(mBattery);
			
			mState = PMStates.Idle;
		}
	}
	
	public void PegButtonPressed()
	{
		if(mState == PMStates.Idle)
		{
			audio.PlayOneShot(playSound);
			// announce to the player the game has begun
			GameObject.Find("hinttext").GetComponent<HintText>()
				.SetHint("Quick! Get the battery at the end of the poles!");
			
			// create the battery
			mBattery = (GameObject)Instantiate (battery, mBatteryLoc, Quaternion.identity);
			
			// start the timer
			mState = PMStates.Playing;
			mTimeRemaining = timer;
		}
		else if(mState == PMStates.Playing)
		{
			audio.PlayOneShot(playSound);
			// reset the timer and let the player know
			GameObject.Find("hinttext").GetComponent<HintText>()
				.SetHint("The timer has been reset. Go!");
			mTimeRemaining = timer;
		}
		else
		{
			GameObject.Find("hinttext").GetComponent<HintText>()
				.SetHint("You already found the battery here.");
		}
	}
}
