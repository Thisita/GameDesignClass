using UnityEngine;
using System.Collections;

public class Animator : MonoBehaviour
{
	
	public float startPosition = -1.0f;
	public float endPosition = 0.5f;
	public float speed = 1.0f;
	
	private float mStartTime;
	
	// Use this for initialization
	void Start ()
	{
		mStartTime = Time.time;
	}
	
	// Update is called once per frame
	void Update ()
	{
		var transpos = transform.position;
		transpos.x = Mathf.Lerp(startPosition, endPosition, (Time.time - mStartTime) * speed);
		transform.position = transpos;
	}
}
