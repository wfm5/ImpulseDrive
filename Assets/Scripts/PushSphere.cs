using UnityEngine;
using System.Collections;
namespace GJ2{
	public class PushSphere : MonoBehaviour {

		private Rigidbody rb;
		public float lifeToLive;
		public float blastRadius;
		public float explosionPower;
		private LayerMask explosionlayers = 1<<13|1<<10|1<<9|1<<8;
		private Collider[] hitcolliders;
		// Use this for initialization
		void Start () {
			rb = GetComponent<Rigidbody>();
			lifeToLive = 2f;
			blastRadius = 5f;
			explosionPower = 10f;
		}
		
		// Update is called once per frame
		void Update () {
			CountdownToDestroy ();
			GetInput ();
		}
		void GetInput()
		{
			if (Input.GetKeyDown (KeyCode.Q)) {
				Explode ();
			}

		}
		void CountdownToDestroy(){
			lifeToLive -= Time.deltaTime;
			if(lifeToLive <= 0 && GetComponent<FixedJoint>() == null)// 
				Destroy (gameObject);
		}
		void PrepAttachToObject()
		{
			rb.isKinematic = false;
			rb.detectCollisions = true;
		}
		void OnCollisionEnter(Collision other)
		{
			PrepAttachToObject ();
			if(other.gameObject.layer == 13 || other.gameObject.layer == 12)
			{
				foreach (ContactPoint contact in other.contacts) {
					FixedJoint fixedJoint = gameObject.AddComponent<FixedJoint> ();
					fixedJoint.anchor = contact.point;
					fixedJoint.connectedBody = other.rigidbody;
				}
			}
					
		}
		void Explode()
		{
			hitcolliders = Physics.OverlapSphere (transform.position,blastRadius, explosionlayers);
			foreach(Collider hitCol in hitcolliders)
			{
				Debug.Log (hitCol.gameObject.name);
				if (hitCol.GetComponent<Rigidbody> () != null) 
				{
					hitCol.GetComponent<Rigidbody> ().AddExplosionForce (explosionPower, transform.position, blastRadius, 1f, ForceMode.Impulse);
				}
			}
			Destroy (gameObject);
		}
	}
}