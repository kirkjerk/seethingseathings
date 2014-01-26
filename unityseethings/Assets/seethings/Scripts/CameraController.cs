using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
	public Transform target ;
	public float distance = 50.0f;
	
	public float  xSpeed = 250.0f;
	public float  ySpeed = 120.0f;
	public float zoomSpeed = 2;

	protected float  yMinLimit = -45;
	protected float  yMaxLimit = 45;
	
//	private float x = 0;
	private float y = 0;

	float driftUpDown = 0.0f;
	
	void Start () {
		var angles = transform.eulerAngles;
		//x = angles.y;
		y = angles.x;
		
		// Make the rigid body not change rotation
		if (rigidbody)
			rigidbody.freezeRotation = true;
	}
	
	void LateUpdate () {

		// x += Input.GetAxis("Horizontal") * xSpeed * 0.02f;
		//distance += Input.GetAxis("Depth") * zoomSpeed;
		//y += Input.GetAxis("Depth") * ySpeed * 0.02f;
		//y = ClampAngle(y, yMinLimit, yMaxLimit);

		//Debug.Log ("y is "+y);

		//distance -= Input.GetAxis("Vertical") * zoomSpeed;

		//transform.rotation = Quaternion.Euler(y, transform.rotation.eulerAngles.x, 0);
		//var position = rotation * new Vector3(0, 0, -distance) + getTargetPosition();
		
		//transform.rotation = rotation;
		//transform.position = position;


		Vector3 rotationVector = transform.rotation.eulerAngles;
		rotationVector.y += Input.GetAxis("Horizontal");
		transform.rotation = Quaternion.Euler(rotationVector);

		driftUpDown -= .0025f;

		if (Input.GetKeyDown ("space")) {
			driftUpDown += .125f;	
		}

		Vector3 driftUpDownVector = new Vector3 (0, driftUpDown, 0);
	

		transform.position = transform.position +  driftUpDownVector + 
			(transform.rotation *  (Vector3.forward * Input.GetAxis("Vertical")));

		float terrainHeight = Terrain.activeTerrain.SampleHeight (transform.position) 
						+ Terrain.activeTerrain.transform.position.y
						+ 10.0f;
		if (transform.position.y < terrainHeight) {
			driftUpDown = 0;
			transform.position = new Vector3 (transform.position.x, terrainHeight, transform.position.z);
		} else {
			transform.position = new Vector3 (transform.position.x, transform.position.y + driftUpDown, transform.position.z);
		}

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
