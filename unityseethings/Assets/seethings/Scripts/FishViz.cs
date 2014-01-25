using UnityEngine;
using System.Collections;

public class FishViz : MonoBehaviour {
	protected Vector3 positionLastFrame ;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void LateUpdate () {


	}
	public void setFacing(Vector3 impulse){
		this.transform.LookAt (transform.position + impulse);
		//Vector3 deltaFromLastFrame = transform.position - positionLastFrame;
		
		//this.transform.LookAt(deltaFromLastFrame + transform.position);
		
		//positionLastFrame = transform.position;
	}
}
