using UnityEngine;
using System.Collections;

public class PointToNorth : MonoBehaviour {

	public GameObject north;
	private Vector3 towardVec;
	public Quaternion LookAt;
	private float maxDegreesPerSecond;
	// Use this for initialization
	void Start () {
		north = GameObject.Find ("NORTH");
		towardVec = north.transform.position - transform.position;
		maxDegreesPerSecond = 10f;
	}
	
	// Update is called once per frame
	void Update () {
		LookAt = Quaternion.LookRotation(towardVec,Vector3.up);
		transform.rotation = Quaternion.RotateTowards (transform.rotation, LookAt, maxDegreesPerSecond * Time.deltaTime);
	}
}
