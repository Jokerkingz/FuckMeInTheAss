using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Health : MonoBehaviour {
	public float vHealth;
	public GameObject vOwner;
	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
	}
	void GetHit(float tDamage){
		vHealth -= tDamage;
		if (vHealth < 0f)
			vOwner.SendMessage ("Die");
	}
}
