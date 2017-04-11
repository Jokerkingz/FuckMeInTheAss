using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_PickUp : MonoBehaviour {
	public int vWhichGun;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(0f,5f,0f);
	}
	void OnTriggerEnter(Collider Other){
		if (Other.tag == "Player") {
			if (vWhichGun == 1) {
				Other.GetComponent<Scr_Player> ().vShotGun = true;
			} else if (vWhichGun == 2) {
				Other.GetComponent<Scr_Player> ().vCannonGun = true;
			}
			DestroyObject (this.gameObject);
		}
	}
}
