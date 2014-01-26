using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
	public Transform target ;
	public float distance = 50.0f;
	
	public float  xSpeed = 250.0f;
	public float  ySpeed = 120.0f;
	public float zoomSpeed = 2;

	public float  yMinLimit = -20;
	public float  yMaxLimit = 80;
	
	private float x = 0;
	private float y = 0;
	
	void Start () {
		var angles = transform.eulerAngles;
		x = angles.y;
		y = angles.x;
		
		// Make the rigid body not change rotation
		if (rigidbody)
			rigidbody.freezeRotation = true;
	}
	
	void LateUpdate () {
		/*
		 x += Input.GetAxis("Horizontal") * xSpeed * 0.02f;
		//y -= Input.GetAxis("Vertical") * ySpeed * 0.02f;
		//distance += Input.GetAxis("Depth") * zoomSpeed;
		//y = ClampAngle(y, yMinLimit, yMaxLimit);

		distance -= Input.GetAxis("Vertical") * zoomSpeed;

		var rotation = Quaternion.Euler(y, x, 0);
		var position = rotation * new Vector3(0, 0, -distance) + getTargetPosition();
		
		transform.rotation = rotation;
		transform.position = position;
		*/

		Vector3 rotationVector = transform.rotation.eulerAngles;
		rotationVector.y += Input.GetAxis("Horizontal");
		transform.rotation = Quaternion.Euler(rotationVector);

		transform.position = transform.position + (transform.rotation *  (Vector3.forward * Input.GetAxis("Vertical")));

	}
	
	static float ClampAngle (float angle, float min, float max) {
		if (angle < -360)
			angle += 360;
		if (angle > 360)
			angle -= 360;
		return Mathf.Clamp (angle, min, max);
	}

	Vector3 getTargetPosition()
	{
		if (target)
			return target.transform.position;
		else 
			return Vector3.zero;
	}


}
