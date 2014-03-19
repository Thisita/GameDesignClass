using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class CoconutThrow : MonoBehaviour
{
	
	public AudioClip throwSound;
	public Rigidbody coconutObject;
	public float throwForce;
	
	public static bool canThrow = false;
	
	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(Input.GetButtonUp("Fire1") && canThrow)
		{
			audio.PlayOneShot(throwSound);
			Rigidbody newCoconut = Instantiate(coconutObject, transform.position, transform.rotation) as Rigidbody;
			newCoconut.name = "coconut";
			newCoconut.rigidbody.velocity = transform.TransformDirection(new Vector3(0, 0, throwForce));
			
			Physics.IgnoreCollision(transform.root.collider, newCoconut.collider);
		}
	}
}
