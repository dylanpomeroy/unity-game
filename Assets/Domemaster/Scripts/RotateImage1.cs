using UnityEngine;
using System.Collections;

public class RotateImage1 : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.RotateAround (Vector3.zero, Vector3.up, -30 * Time.deltaTime);
	}
}
