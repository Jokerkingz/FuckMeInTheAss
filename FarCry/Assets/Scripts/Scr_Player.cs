using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Player : MonoBehaviour {
	// Components // Components // Components // Components // Components // Components // Components // Components // Components // Components 
	[Header("Components")]
	public CharacterController cCC;
	public Scr_GunControl cGC;
	public GameObject vElevator;
	public Canvas vCanvas;
	public GameObject vModel;

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
	public string vTargetName;
	public AnimationCurve vBounce;
	public float vBounceFrame;

	public bool vShotGun;
	public bool vCannonGun;

	// Camera // Camera // Camera // Camera // Camera // Camera // Camera // Camera // Camera // Camera // Camera // Camera // Camera // Camera 
	[Header("Camera")]
	public GameObject ViewBase;
	public Camera vCam;
	public GameObject vTargetSpot;
	private float vSpedH = 2f;
	private float vSpedV = 2f;
	public float vYaw = -90f;
	public float vPitch = 0.0f;

	public LayerMask vLayer = 1;
	public string lMask;

	// GameObjects // GameObjects // GameObjects // GameObjects // GameObjects // GameObjects // GameObjects // GameObjects // GameObjects 
	public bool v1stClear;
	public bool v2ndClear;


	public bool vIntro = true;
	public bool vStart = false;

	public AudioSource[] aSpeakAudios;
	void Start()
	{vActing = true;
	}

	void Awake ()
	{GameObject.DontDestroyOnLoad(transform.gameObject);
	}

	void Update ()
	{	
		vIsCrouching = false;
		vTargetName =  ObjectInfront();
		if (!vActing)
			InputCheck ();
		if (vIntro && !vStart)
			vDirection = new Vector3(1f,0f,0f);
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


		if (vBounceFrame > 0f && cCC.isGrounded)
			vBounceFrame += .05f;
		if (vBounceFrame > 1f) {
			vBounceFrame = 0f;
			// Play Fap sound
		}
		float vYModel = vBounce.Evaluate (vBounceFrame);
		cCC.height = 2f*vCrouch;
		ViewBase.transform.localPosition = new Vector3 (0,vCrouch, 0f);
		vDirection.y = vYSpeed;
		vDirection = transform.TransformDirection (vDirection);
		if (vPenatrating)
			vSpeed = 1f;
		cCC.Move(vDirection *vSpeed* Time.deltaTime);
		vSpeed = 20f;
		transform.eulerAngles = new Vector3 (0f, vYaw-90f,0f);
		vModel.transform.localPosition = new Vector3 (0,-1+(vYModel/2f), 0f);
		ViewBase.transform.localPosition = new Vector3 (0f,1f+(vYModel/2f), 0f);
		ViewBase.transform.eulerAngles = new Vector3 (vPitch,vYaw, 0f);


		if (transform.position.y < -10)
			transform.position = Vector3.zero;
		switch (vTargetName) {
		case "ElevatorButton":
			vCanvas.SendMessage("ShowMessage", "Press E to Open");
			break;
		case "FirstButton":
			vCanvas.SendMessage("ShowMessage", "Press E for first floor");
			break;
		case "SecondButton":
			vCanvas.SendMessage("ShowMessage", "Press E to second floor");

			break;
		case "ThirdButton":
			vCanvas.SendMessage("ShowMessage", "Press E to third floor");

			break;
		}


	}

	string ObjectInfront(){
		
		string tString = "Null";
		Ray tRay = vCam.ScreenPointToRay (new Vector2 (vCam.pixelWidth / 2f, vCam.pixelHeight / 2f));
		RaycastHit tHit;
		if (Physics.Raycast (tRay, out tHit, 2f, vLayer)) {
			Debug.Log ("Check Target");
			switch (tHit.collider.gameObject.tag) {
			case "ElevatorButton":
				tString = "ElevatorButton";
				break;
			case "FirstFloorButton":
				tString = "FirstButton";
				break;
			case "SecondFloorButton":
				tString = "SecondButton";
				break;
			case "ThirdFloorButton":
				tString = "ThirdButton";
				break;
			}
		}
		return tString;
	}

	void InputCheck(){ // Input // Input // Input // Input // Input // Input // Input // Input // Input 

		if (Input.GetKeyDown(KeyCode.Q))
			vCanvas.SendMessage("GetHit",.4f);
		if (Input.GetKeyDown(KeyCode.Alpha1))
			cGC.SwitchGun("Gun");
		if (Input.GetKeyDown(KeyCode.Alpha2) && vShotGun)
			cGC.SwitchGun("Rocket");
		if (Input.GetKeyDown(KeyCode.Alpha3) && vCannonGun)
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
			vYSpeed = .75f;
			Debug.Log ("Jumped");
		}



		/// Mouse Input
		vYaw += vSpedH * Input.GetAxis ("Mouse X");
		vPitch -= vSpedV * Input.GetAxis ("Mouse Y");
		if (vPitch > 90f) vPitch = 90f; 			// Clamp
		if (vPitch < -90f) vPitch = -90f;			// Clamp
		vDirection = new Vector3(Input.GetAxis("Vertical"),0f,(Input.GetAxis("Horizontal")*-1f));
		if ((Input.GetAxis("Vertical") != 0f || Input.GetAxis("Horizontal") != 0f) && !vIsCrouching && cCC.isGrounded)
			vBounceFrame += .001f;



		if (Input.GetKeyDown (KeyCode.E)) {
			Debug.Log ("Elevator Button Open");
			switch (vTargetName) {
			case "ElevatorButton":
				vElevator = GameObject.FindGameObjectWithTag ("Elevator");
				vElevator.SendMessage ("OpenDoor");
				break;
			case "FirstButton":
				Debug.Log ("Elevator Button NextFLoor");
				vElevator = GameObject.FindGameObjectWithTag ("Elevator");
				vElevator.SendMessage ("NextFloor",0);
				break;
			case "SecondButton":
				Debug.Log ("Elevator Button NextFLoor");
				vElevator = GameObject.FindGameObjectWithTag ("Elevator");
				vElevator.SendMessage ("NextFloor",1);

				break;
			case "ThirdButton":
				Debug.Log ("Elevator Button NextFLoor");
				vElevator = GameObject.FindGameObjectWithTag ("Elevator");
				vElevator.SendMessage ("NextFloor",2);

				break;
			}

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


	public void Respawn(){
		vActing = false;
		vCanvas.GetComponent<Scr_Canvas> ().vWhiteOutAlpha = 0f;
		if (v1stClear)
			transform.position = new Vector3 (0f, 0f, 0f);

	}

	public void Speak(int WhichSound){
		aSpeakAudios [WhichSound].Play ();
	}

}
