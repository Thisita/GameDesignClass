using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class BackButtonGUI : MonoBehaviour {
	
	public AudioClip beep;
	public GUISkin menuSkin;
	public float areaWidth;
	public float areaHeight;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnGUI()
	{
		GUI.skin = menuSkin;
		
		float ScreenX = ((Screen.width / 2f) - (areaWidth / 2f));
		float ScreenY = ((Screen.height / 1.7f) - (areaHeight / 2f));
		
		GUILayout.BeginArea(new Rect(ScreenX, ScreenY, areaWidth, areaHeight));
		
		if(GUILayout.Button("Back"))
		{
			StartCoroutine(OpenLevel("MenuScene"));
		}
		
		GUILayout.EndArea();
	}
	
	IEnumerator OpenLevel(string level)
	{
		audio.PlayOneShot(beep);
		yield return new WaitForSeconds(0.35f);
		Application.LoadLevel(level);
	}
}
