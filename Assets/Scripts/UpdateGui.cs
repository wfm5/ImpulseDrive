using UnityEngine;
using System.Collections;
namespace GJ2{
	public class UpdateGui : MonoBehaviour {

		private string currentEmitter;
		private string directionVector;

		public GameObject impulseSphere;
		private int equippedGrenade;
		private int index;

		// Use this for initialization
		void Start () {
		}
		
		// Update is called once per frame
		void Update () {
			
			equippedGrenade = GameObject.Find ("Player").GetComponent<ThrowSphere> ().equippedGrenade;
			index = ForceSphere.index;
			if (equippedGrenade == 0) 
			{
				currentEmitter = "Impulse";
				GetComponentInChildren<UnityEngine.UI.Text>().text = "Current Shock Wave Emitter: " + currentEmitter;
			}
			else if(equippedGrenade == 1)
			{
				currentEmitter = "Explosion";
				GetComponentInChildren<UnityEngine.UI.Text>().text = "Current Shock Wave Emitter: " + currentEmitter;

			}

			directionVector = "Direction Vector: " + impulseSphere.GetComponent<ForceSphere> ().directionVector[index].ToString ();
			Debug.Log ("Update GUI "+directionVector);
			Debug.Log ("Update GUI "+index);

			GetComponentsInChildren<UnityEngine.UI.Text> () [1].text = directionVector;

		}
	}
}