using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class CoconutWin : MonoBehaviour
{
	
	public static int targets = 0;
	public AudioClip win;
	public GameObject battery;
	
	private bool haveWon = false;
	
	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(targets == 3 && !haveWon)
		{
			audio.PlayOneShot(win);
			
			Instantiate(battery,
				new Vector3(
					transform.position.x,
					transform.position.y + 2,
					transform.position.z),
				transform.rotation);
			
			haveWon = true;
		}
		
	}
}
