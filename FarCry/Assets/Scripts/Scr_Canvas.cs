using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scr_Canvas : MonoBehaviour {
	public GameObject vPlayer;
	public Image vWhiteOut;
	public float vWhiteOutAlpha;
	public Image vStain;
	public float vStainAlpha;
	public Text vText;
	public float vTextAlpha;
	public bool vDead;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		if (vWhiteOutAlpha > 0f)
			vWhiteOutAlpha -= .001f;
		else
			vWhiteOutAlpha = 0f;
		vWhiteOut.GetComponent<CanvasGroup>().alpha = vWhiteOutAlpha;



		if (vTextAlpha > 0f)
			vTextAlpha -= .05f;
		vText.GetComponent<CanvasGroup>().alpha = vTextAlpha;

		if (vDead) {
			if (vStainAlpha < 1f)
				vStainAlpha += .01f;
			else {
				// reset room;
				vDead = false;
				vPlayer.SendMessage ("Respawn");
			}
		} else {
			if (vStainAlpha > 0f)
				vStainAlpha -= .02f;
			else
				vStainAlpha = 0f;

		}
		vStain.GetComponent<CanvasGroup>().alpha = vStainAlpha;
	}

	public void ShowMessage(string tString){
		if (vTextAlpha < 1f)
			vTextAlpha += .1f; 
		vText.text = tString;
	}

	public void GetHit(float tFloat){
		if (!vDead) {
			vWhiteOutAlpha += tFloat;
			if (vWhiteOutAlpha > 1f){
				vDead = true;
				vPlayer.GetComponent<Scr_Player>().vActing = true;
			}
		}
	}
}
