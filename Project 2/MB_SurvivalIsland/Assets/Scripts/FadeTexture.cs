using UnityEngine;
using System.Collections;

public class FadeTexture : MonoBehaviour
{

	public Texture2D theTexture;
	public float fadeTime = 3.0f;
	
	private float mStartTime;
	
	// Use this for initialization
	void Start ()
	{
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(Time.time - mStartTime >= fadeTime)
		{
			Destroy(gameObject);
		}
	}
	
	void OnGUI()
	{
		var newColor = Color.white;
		newColor.a = Mathf.Lerp(1.0f, 0.0f, (Time.time - mStartTime) / fadeTime);
		
		GUI.color = newColor;
		GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), theTexture);
	}
	
	void OnLevelWasLoaded()
	{
		mStartTime = Time.time;
	}
}
