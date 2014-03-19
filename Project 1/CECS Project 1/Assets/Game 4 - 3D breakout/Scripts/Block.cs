using UnityEngine;
using System.Collections;

public class Block : MonoBehaviour {

	void OnTriggerEnter () {
        AudioSource.PlayClipAtPoint(gameObject.audio.clip, gameObject.transform.position);
        BreakoutGame.SP.HitBlock();
        Destroy(gameObject);
	}
}
