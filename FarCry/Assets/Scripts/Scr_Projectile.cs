using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Projectile : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		//transform.position = new Vector3 (transform.position.x, transform.position.y + 1f, transform.position.z);
	}
	void OnCollisionEnter(){
		this.GetComponent<Scr_Vibrator> ().enabled = true;
		this.GetComponent<Rigidbody> ().useGravity = true;
		this.GetComponent<Scr_Projectile> ().enabled = false;
	}
}
