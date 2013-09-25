using UnityEngine;
using System.Collections;

public class Lava : MonoBehaviour {

	void OnCollisionEnter (Collision collision) {
		
		// If the player touches the lava, squish 'em!
		if(collision.rigidbody.tag == "Player")
		{
			collision.rigidbody.GetComponent<Player>().Squish();
		}
    }
}
