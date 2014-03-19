using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class MainMenuGUI2 : MonoBehaviour
{
	
	public AudioClip beep;
	public GUISkin menuSkin;
	public float areaWidth;
	public float areaHeight;
	
	private float mScreenX;
	private float mScreenY;
	private Rect mScreenRect;
	
	// Use this for initialization
	void Start ()
	{
		mScreenX = ((Screen.width * 0.5f) - (areaWidth * 0.5f));
		mScreenY = ((Screen.height * 0.5f) - (areaHeight * 0.5f));
		mScreenRect = new Rect(mScreenX, mScreenY, areaWidth, areaHeight);
	}
	
	// Update is called once per frame
	void Update ()
	{
	}
	
	void OnGUI()
	{
		GUI.skin = menuSkin;
		
		GUILayout.BeginArea(mScreenRect);
		
		if(GUILayout.Button("Play"))
			StartCoroutine(OpenLevel("MainIsland"));
		
		if(GUILayout.Button("Instructions"))
			StartCoroutine(OpenLevel("Instructions"));
		
		if(GUILayout.Button("Quit"))
			Application.Quit();
		
		GUILayout.EndArea();
	}
	
	IEnumerator OpenLevel(string level)
	{
		audio.PlayOneShot(beep);
		yield return new WaitForSeconds(0.35f);
		Application.LoadLevel(level);
	}
}
