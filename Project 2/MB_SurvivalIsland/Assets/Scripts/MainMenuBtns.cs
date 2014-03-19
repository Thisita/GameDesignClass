using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class MainMenuBtns : MonoBehaviour
{
	
	public string levelToLoad;
	public Texture2D normalTexture;
	public Texture2D rollOverTexture;
	public AudioClip beep;
	public bool isQuitButton;
	
	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
	
	void OnMouseEnter()
	{
		guiTexture.texture = rollOverTexture;
	}
	
	void OnMouseExit()
	{
		guiTexture.texture = normalTexture;
	}
	
	IEnumerator OnMouseUp()
	{
		audio.PlayOneShot(beep);
		yield return new WaitForSeconds(0.35f);
		
		if(isQuitButton)
		{
			Application.Quit();
		}
		else
		{
			Application.LoadLevel(levelToLoad);
		}
	}
}
