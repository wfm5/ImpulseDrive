using UnityEngine;
using System.Collections;

public class ThrowSphere : MonoBehaviour {

	private GameObject go;
	public Transform myTransform;
	public float propulsionForce = 5f;

	public GameObject[] grenadesArsenal;
	public int equippedGrenade; //should only be 0 or 1

	// Use this for initialization
	void Start () {
		myTransform = GameObject.Find("Player").transform;
		equippedGrenade = 0;
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log ("Equiped Grenade: "+ equippedGrenade);
		ClickCommands ();
		SetGrenade ();

	}

	void ClickCommands()
	{
		if (Input.GetButtonDown ("Fire2")) {
			Debug.DrawRay (transform.position, transform.GetComponentInChildren<Camera> ().transform.forward, Color.green, 3f);
			//if(Physics.Raycast(transform.position,transform.GetComponentInChildren<Camera>().transform.forward,out hit,5f,forceLayer))
			//{
			SpawnGrenade ();
			//}
		}
	}

	void SpawnGrenade()
	{
		if (equippedGrenade == 0) {
			
			go = Instantiate (grenadesArsenal [equippedGrenade], myTransform.GetComponentInChildren<Camera> ().transform.TransformPoint (0f, 0f, 4f), myTransform.rotation) as GameObject;

		}
		else if(equippedGrenade == 1)
		{
			go = (GameObject)Instantiate (grenadesArsenal [equippedGrenade], myTransform.GetComponentInChildren<Camera>().transform.TransformPoint (0f, 0f, 2f),myTransform.rotation);
			go.GetComponent<Rigidbody> ().AddForce (myTransform.GetComponentInChildren<Camera>().transform.forward*propulsionForce,ForceMode.Impulse);
		}
	}
	void SetGrenade(){
		if (Input.GetKeyDown (KeyCode.E)) {
			if(equippedGrenade > 0){
				equippedGrenade = 0;
			}else{
				equippedGrenade=1;
			}
		}

	}

}
