using UnityEngine;
using System.Collections;

public class PlayerInput : MonoBehaviour {


    public AudioClip canHitSound;

	void Update () {
        if (GameManager.gameState == GameState.playing)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 100))
                {

                    Debug.DrawLine(ray.origin, hit.point);

                    if (hit.transform.tag == "ShootingObject")
                    {
                        audio.pitch = Random.Range(0.9f, 1.3f);
                        audio.Play();
                        GameObject explosion = GameObject.Find("Explosion");
                        explosion.particleSystem.Stop();
                        explosion.transform.position = hit.transform.position;
                        explosion.particleSystem.Play();
                        GameManager.SP.RemoveObject(hit.transform);
                    }
                    else if (hit.transform.tag == "Can")
                    {
                        audio.PlayOneShot(canHitSound);

                        Vector3 explosionPos = transform.position;
                        hit.rigidbody.AddExplosionForce(5000, explosionPos, 25.0f, 1.0f);
                    }

                }
            }
        }
	}
}
