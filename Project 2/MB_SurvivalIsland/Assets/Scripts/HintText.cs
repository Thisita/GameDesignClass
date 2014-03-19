using UnityEngine;
using System.Collections;

public class HintText : MonoBehaviour
{
	
	private float mTime = 0.0f;
	private Transform mOrigTrans;
	
	public float hintTime = 3.0f;
	
	void Start ()
	{
		guiText.enabled = false;
	}
	
	void Update ()
	{
		// update if needed
		if(mTime > 0) mTime -= Time.deltaTime;
		
		if(mTime <= 0) // hint time has expired
		{
			guiText.enabled = false;
			mTime = 0;
		}
	}
	
	public void SetHint(string hint)
	{
		mTime = hintTime;
		guiText.text = hint;
		guiText.enabled = true;
	}

}
