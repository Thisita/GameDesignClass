using UnityEngine;
using System.Collections;

public class DestroyTimer : MonoBehaviour {
	
	public float objectLifetime = 5;
	
	// Use this for initialization
	void Start ()
	{
		Destroy(gameObject, objectLifetime);
	}
	
	// Update is called once per frame
	void Update ()
	{
	}
}
