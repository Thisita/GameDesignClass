using UnityEngine;
using System.Collections;

public class BatteryCollect : MonoBehaviour
{
	
	public Texture2D charge1tex;
	public Texture2D charge2tex;
	public Texture2D charge3tex;
	public Texture2D charge4tex;
	public Texture2D charge0tex;
	
	public static int charge = 0;
	
	// Use this for initialization
	void Start ()
	{
		guiTexture.enabled = false;
		charge = 0;
	}
	
	// Update is called once per frame
	void Update ()
	{
		switch(charge)
		{
		case 1:
			guiTexture.texture = charge1tex;
			guiTexture.enabled = true;
			break;
		case 2:
			guiTexture.texture = charge2tex;
			break;
		case 3:
			guiTexture.texture = charge3tex;
			break;
		case 4:
			guiTexture.texture = charge4tex;
			break;
		}
	}
}
