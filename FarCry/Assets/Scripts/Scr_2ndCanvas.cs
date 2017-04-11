using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_2ndCanvas : MonoBehaviour {
	public GameObject vPlayer;
	public int vIsFade;
	public GameObject vBack;
	public GameObject vFade;
	public float vFadeAlpha;
	public GameObject vStain;
	public GameObject vTitle;

	public GameObject vMessage;
	public float vMessageAlpha;

	public AudioSource vAudioSplat;

	public bool vStartGame;
	public bool vDone;
	// Use this for initialization
	void Start () {
		vIsFade = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (!vStartGame){
			if (vMessageAlpha < 1f)
				vMessageAlpha += 0.03f;
			else{
				if (Input.anyKey) {
					vStartGame = true;
					vIsFade = 1;
					vPlayer.GetComponent<Scr_Player>().Speak(0);
					vStain.GetComponent<CanvasGroup> ().alpha = 1;
				}
			}
		}
		
		if (vIsFade == 1) {
			if (vFadeAlpha < 1f)
				vFadeAlpha += 0.01f;
			else {
				if (!vDone) {
					vTitle.GetComponent<CanvasGroup> ().alpha = 0f;
					vStain.GetComponent<CanvasGroup> ().alpha = 0f;
					vPlayer.GetComponent<Scr_Player> ().Speak (1);
					vFadeAlpha = 1f;
					DestroyObject (vMessage);
					DestroyObject (vBack);
					Invoke ("StartGame", 20f);
					vDone = true;
					vIsFade = 0;
				} 
			}

		} else if (vIsFade == 2) {
			if (vFadeAlpha > 0f)
				vFadeAlpha -= 0.01f;
			else {


				vPlayer.GetComponent<Scr_Player> ().vIntro = true;
				vPlayer.GetComponent<Scr_Player> ().vStart = false;
				DestroyObject (this.gameObject);

			}
		}

		vMessage.GetComponent<CanvasGroup> ().alpha = vMessageAlpha;
		vFade.GetComponent<CanvasGroup> ().alpha = vFadeAlpha;

	}
	void StartGame(){
		vIsFade = 2;
		vFadeAlpha = 1f;
		Cursor.lockState = CursorLockMode.Locked;

	}
}
