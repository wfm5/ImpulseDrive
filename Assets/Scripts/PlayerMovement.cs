using UnityEngine;
using System.Collections;
namespace GJ2{
	public class PlayerMovement : MonoBehaviour 
	{
		public Vector3 forwardVec;
		public Vector3 rightVec;

		public float mouseSensitivity = 100.0f;
		public float clampAngle = 80.0f;

		private float rotY;
		private float rotX;

		private Rigidbody myRigibody;
		private RaycastHit hit;
		private float downDist = 1.5f;
		//private bool isGrounded;

		private LayerMask floorLayer = 1 << 12;
		private LayerMask forceLayer = 1 << 11;

		public float speed = 5f;

		// Use this for initialization
		void Start () 
		{
			Cursor.lockState = CursorLockMode.Locked;
			//for camera
			Vector3 rot = transform.localRotation.eulerAngles;
			downDist = 0.5f;

			rotY = rot.y;
			rotX = rot.x;

			myRigibody = gameObject.GetComponent<Rigidbody> ();

			speed = 5f;
		}
		
		// Update is called once per frame
		void Update () 
		{			
			RotateCamera ();
			Move ();
			CheckVelocity (); //??? learn to slow down
			ClickCommands ();

			//Debug.Log (isGrounded);
			if(Input.GetKeyDown(KeyCode.Space))
			{			
	//			if (isGrounded) {
	//				//myRigibody.AddForce (new Vector3 (0, 5f, 0), ForceMode.Impulse);
	//			}
			}
		}
		void ClickCommands()
		{
			if(Input.GetButton("Fire1"))
			{
				Debug.DrawRay (transform.position, transform.GetComponentInChildren<Camera>().transform.forward, Color.green, 3f);
				if(Physics.Raycast(transform.position,transform.GetComponentInChildren<Camera>().transform.forward,out hit,10f,forceLayer))
				{
					Destroy(hit.collider.gameObject);
				}
			}

				
		}
		#region Camera Code
		void RotateCamera()
		{
			float mouseX = Input.GetAxis ("Mouse X");
			float mouseY = Input.GetAxis ("Mouse Y");

			rotY += mouseX *mouseSensitivity* Time.deltaTime;
			rotX += mouseY *mouseSensitivity* Time.deltaTime;

			rotX = Mathf.Clamp (rotX, -clampAngle, clampAngle);

			Quaternion localRotation = Quaternion.Euler (-rotX, rotY, 0.0f);
			gameObject.GetComponentInChildren<Camera>().transform.localRotation= localRotation;

			//mouse Stuff
			bool mouseOverWindow = Input.mousePosition.x>0 && Input.mousePosition.x < Screen.width && Input.mousePosition.y >0 && Input.mousePosition.y < Screen.height;

			if(Cursor.lockState == CursorLockMode.Locked && !mouseOverWindow){
				Cursor.lockState = CursorLockMode.Locked;
			}
			if(Input.GetKeyDown(KeyCode.Escape) && Cursor.lockState == CursorLockMode.Locked)
			{
				Cursor.lockState = CursorLockMode.None;
			}else if(Cursor.lockState == CursorLockMode.None)
			{
				if(mouseOverWindow && (Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1) || Input.GetMouseButtonUp(2)))
				{
					Cursor.lockState = CursorLockMode.Locked;
				}
			}

		}
		#endregion

		void Move()
		{		
			forwardVec = GetComponentInChildren<Camera> ().transform.forward;
			rightVec = GetComponentInChildren<Camera> ().transform.right;
			forwardVec.y = 0f;
			if(Input.GetKey(KeyCode.W))
				transform.Translate(forwardVec*Time.deltaTime*5f);
			if(Input.GetKey(KeyCode.S))
				transform.Translate(-forwardVec*Time.deltaTime*5f);
			if(Input.GetKey(KeyCode.D))
				transform.Translate(rightVec*Time.deltaTime*5f);
			if(Input.GetKey(KeyCode.A))
				transform.Translate(-rightVec*Time.deltaTime*5f);
		}

		void OnCollisionEnter(Collision other)
		{
			if(other.gameObject.layer == 12)
			{
				if (Physics.Raycast (transform.position, -transform.up, downDist, floorLayer)) 
				{
					//isGrounded = true;
				} 
				else 
				{
					//isGrounded = false;
				}
			}
		}

		void CheckVelocity()
		{
			if (myRigibody.velocity.sqrMagnitude > -40)
				myRigibody.velocity.Set(0,-40,0);

		}

	}
}