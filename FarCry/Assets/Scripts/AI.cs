using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour {

	//Agent go to
	public GameObject goHere;
	public Transform goHereTrans;

	//bools
	public bool lookfornew;

	//other codes
	public AIStateMachine sm;

	public float fpsTargetDistance;
	public float enemyLookDistance;
	public float attackDistance;
	public float enemyMovementSpeed;
	public float damping;
	public Transform fpsTarget;
	Rigidbody theRigidbody;
	Renderer myRender;
	CharacterController myController;
	public Vector3 lookAtPosition;
	public Transform player;
	//public GameObject possibleCovers;
	public Transform potentialTarget; 

	public Transform possibleCoversParent;

	//cover spots
	public Transform spotOne;
	public Transform spotTwo;
	public Transform spotThree;
	public Transform spotFour;
	//center of cover to find which side to hide
	public Transform centerOne;
	public Transform centerTwo;

	public UnityEngine.AI.NavMeshAgent agent; 



	// Use this for initialization
	void Start () {
		myRender = GetComponent<Renderer> ();
		theRigidbody = GetComponent<Rigidbody> ();
		myController = GetComponent<CharacterController> ();
		agent = GetComponent<UnityEngine.AI.NavMeshAgent> ();
		lookfornew = true;
	}


	// Update is called once per frame
	void Update () {

		//goHere = goHereTrans.gameObject;

		if (goHere == null) {
			agent.destination = goHereTrans.position;
		} else {
			agent.destination = goHere.transform.position;
		}

		fpsTargetDistance = Vector3.Distance (fpsTarget.position, transform.position);
		/*if (fpsTargetDistance < enemyLookDistance && lookfornew == true) {
			myRender.material.color = Color.yellow;
			findCover ();
			lookfornew = false;

		}
		if (fpsTargetDistance < attackDistance) {
			myRender.material.color = Color.red;
			attackPlease ();
		} else {
			myRender.material.color = Color.blue;
		}
		*/
	}

	//is actually "see player and find cover"
	public void findCover(){

		//GameObject[] covs = GameObject.FindGameObjectsWithTag ("cover");
		//for(int i= 0, i < covs, i++){
			
		GameObject gos = GameObject.FindGameObjectWithTag ("Cover");
		Transform bestTarget = GetClosestCover (gos.GetComponents<Transform> ()/*to change number of possible covers, add ",#"*/);

		goHere = bestTarget.GetChild(0).gameObject; 
		//sm.StartAttack ();
		Debug.Log (bestTarget.position);
		//findActualCover ();
	}

	/*GameObject findActualCover(){
		GameObject[] gos;
		gos = GameObject.FindGameObjectsWithTag ("cover");
		GameObject closest;
		float distance = Mathf.Infinity;
		foreach (GameObject go in gos) {
			Vector3 diff = go.transform.position - this.transform.position;
			float curDistance = diff.sqrMagnitude;
			if (curDistance < distance) {
				closest = go;
				distance = curDistance;
			}
		}
		return closest;
	}
*/

	Transform GetClosestCover(Transform[] PossibleCovers, int bestOf = 3){
		if (PossibleCovers.Length < bestOf) {
			bestOf = PossibleCovers.Length;
		}

		Transform[] bestTargets = new Transform[bestOf];
		float[] closestDistanceSqr = new float[bestOf];

		for (int i = 0; i < bestOf; i++) {
			closestDistanceSqr[i] = (Mathf.Infinity);
			bestTargets[i] = (null);
		}
			Vector3 currentPosition = transform.position;
		foreach (Transform potentialCover in PossibleCovers) {
			Vector3 directionToTarget = player.position - currentPosition;
			float dSqrToTarget = directionToTarget.sqrMagnitude;
			int worstCase = -1;
			for (int i = 0; i < bestOf; i++) {
				if (dSqrToTarget < closestDistanceSqr [i]) {
					if (worstCase != -1) {
						if (closestDistanceSqr [i] > closestDistanceSqr [worstCase]) {
							worstCase = i;
							}		
					} else {
						worstCase = i;
						}
				}
			}
			if (worstCase != -1) {
				closestDistanceSqr [worstCase] = dSqrToTarget;
				bestTargets [worstCase] = potentialCover;
			}
		
		}
		return bestTargets[Random.Range(0,bestOf)];
		
	}
	

	void attackPlease(){
		Vector3 forward = transform.TransformDirection (Vector3.forward);
		
		myController.Move(forward * enemyMovementSpeed * Time.deltaTime);


		if (transform.position.y < -10)
			transform.position = Vector3.zero;
	}

}
