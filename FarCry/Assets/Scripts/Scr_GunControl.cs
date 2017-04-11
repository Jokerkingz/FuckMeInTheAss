using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_GunControl : MonoBehaviour {
	// Weapons // Weapons // Weapons // Weapons // Weapons // Weapons // Weapons // Weapons // Weapons // Weapons // Weapons // Weapons // Weapons 
	public string vCurrentWeapon = "Shocker";
	public string vStatus;
	public float vCoolDown; // 0f = ready;
	public GameObject tGO; // 0f = ready;
	public Camera vCam; // 0f = ready;
	public GameObject ViewBase;
	public GameObject vSpawnSpot;
	public LayerMask vLayer;
	public float vOffSet = 5f;
	private Color[] vColors;


	[Header("Bullet")]
	public GameObject vBulletA;
	public GameObject vBulletB;
	public GameObject vBulletC;
	public GameObject vBulletD;

	[Header("Weapon")]
	// Shoocker
	public GameObject vWeaponA; 		// First Weapon
	public bool vHasWepA; 				// Has The Weapon

	// Gun?
	public GameObject vWeaponB; 		// Second Weapon
	public bool vHasWepB; 				// Has The Weapon

	// Rocket?
	public GameObject vWeaponC; 		// Third Weapon
	public bool vHasWepC; 				// Has The Weapon

	// Grenades
	public GameObject vWeaponD; 		// Grenade Weapon
	public bool vHasWepD; 				// Has The Weapon


	// Use this for initialization
	void Start () {
		vColors = new Color[9];
		vColors [0] = Color.white;
		vColors [1] = Color.blue;
		vColors [2] = Color.black;
		vColors [3] = Color.red;
		vColors [4] = Color.green;
		vColors [5] = Color.cyan;
		vColors [6] = Color.clear;
		vColors [7] = Color.magenta;
		vColors [8] = Color.yellow;
	}
	
	// Update is called once per frame
	void Update () {
		if (vCoolDown > 0f)
			vCoolDown -= .2f;
		else
		{vCoolDown = 0f;
				if (vStatus == "Shoot")
					vStatus = "Ready";
			}
				
			
	}
	public void SwitchGun(string tWeapon){
		tGO = null;
		switch (tWeapon) {
		case "Shocker":
			if (!vHasWepA)
				return;
			else
				tGO = vWeaponA;
			break;
		case "Gun":
			if (!vHasWepB)
				return;
			else
				tGO = vWeaponB;
			break;
		case "Rocket":
			if (!vHasWepC)
				return;
			else
				tGO = vWeaponC;
			break;
		case "Grenade":
			if (!vHasWepD)
				return;
			else
				tGO = vWeaponD;
			break;

		}
		if (vCurrentWeapon != tWeapon) {
			vCurrentWeapon = tWeapon;
			vCoolDown = 0f;
			vWeaponA.SetActive (false);
			vWeaponB.SetActive (false);
			vWeaponC.SetActive (false);
			vWeaponD.SetActive (false);
			tGO.SetActive (true);

			// Animate
			vStatus = "FirstAni";
		}
			
	}
	public void ShootGun(){
		RaycastHit tHit;
		Ray tRay;
		Vector3 tVect3;
		float tOff = vOffSet*.5f*(Screen.height / 200);
		if (vStatus == "FirstAni"){
			vStatus = "Ready";
		}
		if (vStatus == "Ready")
		switch (vCurrentWeapon) {
		case "Shocker":
			// Poke attack


			break;
			case "Gun":
				tRay = vCam.ScreenPointToRay (new Vector2 ((vCam.pixelWidth / 2f) + (Random.Range (-tOff, tOff) * 2f), (vCam.pixelHeight / 2f) + (Random.Range (-tOff, tOff) * 2f)));
				tVect3 = ViewBase.transform.eulerAngles;
				if (Physics.Raycast (tRay, out tHit, 100f, vLayer)) {
					tGO = Instantiate (vBulletB);
					tGO.transform.position = tHit.point;
				}
				vStatus = "Shoot";
				vCoolDown = 1f;
			break;
			case "Rocket":
				int vCount = 20;
				while (vCount > 0) {
					tRay = vCam.ScreenPointToRay (new Vector2 ((vCam.pixelWidth / 2f) + (Random.Range (-tOff, tOff) * 5f), (vCam.pixelHeight / 2f) + (Random.Range (-tOff, tOff) * 5f)));
					Debug.Log (tRay.direction);
					tVect3 = ViewBase.transform.eulerAngles;
					if (Physics.Raycast (tRay, out tHit, 100f, vLayer)) {
						tGO = Instantiate (vBulletB);
						tGO.transform.position = tHit.point;
					}
					vCount -= 1;
				}
				vStatus = "Shoot";
				vCoolDown = 3f;
			break;
			case "Grenade":
				tGO = Instantiate (vBulletD);
				tGO.transform.position = vSpawnSpot.transform.position;
				tGO.transform.localRotation = vSpawnSpot.transform.rotation;
				tGO.GetComponent<Rigidbody> ().AddForce (tGO.transform.up * 2000f);
				Renderer rend = tGO.GetComponentInChildren<Renderer>();
				rend.material.SetColor("_SpecColor",vColors[Random.Range(0,8)]);
			vStatus = "Shoot";
			vCoolDown = 3f;
			break;

		}


	}
}
