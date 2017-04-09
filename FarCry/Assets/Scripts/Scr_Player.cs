using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Player : MonoBehaviour {
	// Components // Components // Components // Components // Components // Components // Components // Components // Components // Components 
	[Header("Components")]
	public CharacterController cCC;
	public Scr_GunControl cGC;
	public GameObject vElevator;

	// Variables // Variables // Variables // Variables // Variables // Variables // Variables // Variables // Variables // Variables // Variables 
	[Header("Variables")]
	private float vYSpeed = -1;
	private Vector3 vDirection = Vector3.zero;
	public float vSpeedMultiplier = 2f;
	public float vSpeed = 20f;
	public bool vActing;
	public bool vMouseLock;
	public bool vIsCrouching;
	public float vCrouch = 1f;
	private bool vPenatrating;

	// Camera // Camera // Camera // Camera // Camera // Camera // Camera // Camera // Camera // Camera // Camera // Camera // Camera // Camera 
	[Header("Camera")]
	public GameObject ViewBase;
	public Camera vCam;
	public GameObject vTargetSpot;
	private float vSpedH = 2f;
	private float vSpedV = 2f;
	public float vYaw = 90.0f;
	public float vPitch = 0.0f;

	public LayerMask vLayer = 1;// << 8;
	public string lMask;

	// GameObjects // GameObjects // GameObjects // GameObjects // GameObjects // GameObjects // GameObjects // GameObjects // GameObjects 


	void Start()
	{
	}

	void Awake ()
	{GameObject.DontDestroyOnLoad(transform.gameObject);
	}

	void Update ()
	{	
		vIsCrouching = false;
		if (!vActing)
			InputCheck ();
		if (cCC.isGrounded) {
			if (vYSpeed < -1f)
			vYSpeed = -1f;
		}
		else {if (vYSpeed > -50f)
			vYSpeed -= .05f;
		}

		if (vIsCrouching) {
			if (vCrouch > .5f)
				vCrouch -= .05f;
		} else {
			if (vCrouch < 1f)
				vCrouch += .05f;
			else
				vCrouch = 1f;
		}

		//transform.localScale = new Vector3 (1f, vCrouch, 1f);
		cCC.height = 2f*vCrouch;
		ViewBase.transform.localPosition = new Vector3 (0,vCrouch, 0f);
		vDirection.y = vYSpeed;
		vDirection = transform.TransformDirection (vDirection);
		if (vPenatrating)
			vSpeed = 1f;
		cCC.Move(vDirection *vSpeed* Time.deltaTime);
		vSpeed = 20f;
		transform.eulerAngles = new Vector3 (0f, vYaw-90f,0f);
		ViewBase.transform.eulerAngles = new Vector3 (vPitch,vYaw, 0f);
		if (transform.position.y < -10)
			transform.position = Vector3.zero;



	}


	void InputCheck(){ // Input // Input // Input // Input // Input // Input // Input // Input // Input 
		
		if (Input.GetKeyDown(KeyCode.Alpha1))
			cGC.SwitchGun("Shocker");
		if (Input.GetKeyDown(KeyCode.Alpha2))
			cGC.SwitchGun("Gun");
		if (Input.GetKeyDown(KeyCode.Alpha3))
			cGC.SwitchGun("Rocket");
		if (Input.GetKeyDown(KeyCode.Alpha4))
			cGC.SwitchGun("Grenade");

		if (Input.GetMouseButton (0)) {
			
			cGC.ShootGun ();
		}
		if (Input.GetMouseButtonDown (1)) {
			if (vMouseLock) {
				Cursor.lockState = CursorLockMode.None;
				vMouseLock = false;
			} else {
				Cursor.lockState = CursorLockMode.Locked;
				vMouseLock = true;
				
			}
		}
		if (Input.GetKey (KeyCode.LeftShift))
			vSpeed *= vSpeedMultiplier;
		else if (Input.GetKey (KeyCode.LeftControl)) {
			vIsCrouching = true;
			vSpeed *= .5f;
		}

		if (Input.GetKeyDown (KeyCode.Space) && cCC.isGrounded) {
			vYSpeed = 1f;
			Debug.Log ("Jumped");
		}



		/// Mouse Input
		vYaw += vSpedH * Input.GetAxis ("Mouse X");
		vPitch -= vSpedV * Input.GetAxis ("Mouse Y");
		if (vPitch > 90f) vPitch = 90f; 			// Clamp
		if (vPitch < -90f) vPitch = -90f;			// Clamp
		vDirection = new Vector3(Input.GetAxis("Vertical"),0f,(Input.GetAxis("Horizontal")*-1f));





		if (Input.GetKeyDown (KeyCode.E)) {
			Debug.Log ("Elevator Button Open");
			vElevator = GameObject.FindGameObjectWithTag ("Elevator");
			vElevator.SendMessage ("OpenDoor");
		}

		if (Input.GetKeyDown (KeyCode.Q)) {
			Debug.Log ("Elevator Button NextFLoor");
			vElevator = GameObject.FindGameObjectWithTag ("Elevator");
			vElevator.SendMessage ("NextFloor",2);
		}


	}

	void OnTriggerEnter (Collider Other){
		if (Other.tag == "TightZone") {
			vPenatrating = true;
			Debug.Log ("Penetration");
		}

	}
	void OnTriggerExit (Collider Other){
		if (Other.tag == "TightZone") {
			vPenatrating = false;
		}
	}
}
