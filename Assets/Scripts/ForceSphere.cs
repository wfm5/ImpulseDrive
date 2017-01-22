using UnityEngine;
using System.Collections;
namespace GJ2{
	public class ForceSphere : MonoBehaviour {

		public static int index;
		private float nextCheck;
		private float checkRate;
		public float range;
		public LayerMask AddForceLayers;
		[SerializeField]
		Vector3[] directionVector;

		// Use this for initialization
		void Start () 
		{
			index = 0;
			range = 2f;
			AddForceLayers = 1 << 8;
			checkRate = 2f;
			//directionVector = Vector3 (0, 20, 0);
			StartCoroutine(DelayImpulseStream());
		}
		// Update is called once per frame
		void Update () 
		{			
			DetermineDirection ();
			ImpulseSpherePush();
		}
		void ImpulseSpherePush()
		{
			if (Time.time > nextCheck) 
			{
				nextCheck = Time.time + checkRate;
				Collider[] colliders;
				colliders = Physics.OverlapSphere (transform.position, range, AddForceLayers);
				if (colliders.Length > 0) 
				{
					for (int i = 0; i < colliders.Length; i++) 
					{
						colliders [i].gameObject.GetComponent<Rigidbody> ().AddForce (directionVector [index], ForceMode.Impulse);
					}
				}
				else 
				{
					//Debug.Log ("no collision");
				}
			}
		}
		void DetermineDirection()
		{			
			if(Input.GetKeyDown(KeyCode.G))
			{
				index++;
			}
			if(Input.GetKeyDown(KeyCode.F))
			{
				index--;
			}
			if (index > 4)
				index = 4;
			if (index < 0)
				index = 0;
			
			Debug.Log ("Force Array Index: "+index);
		}
		IEnumerator DelayImpulseStream()
		{
			yield return new WaitForSeconds (2);
		}
	}
}