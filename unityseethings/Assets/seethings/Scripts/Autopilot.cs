using UnityEngine;
using System.Collections;

public class Autopilot : MonoBehaviour {

	public Bounds AllowedArea = new Bounds(Vector3.zero, new Vector3(200,30,200));
	public float Speed = 2;
	public float Angle = 0;
	public float MaxTurnDegreesPerSecond = 45;

	// Update is called once per frame
	void FixedUpdate () {

		// Yay!  In bounds.
		float turn = Random.Range(-MaxTurnDegreesPerSecond * Time.fixedDeltaTime, MaxTurnDegreesPerSecond * Time.fixedDeltaTime);
		Angle += turn;

		this.transform.rotation = Quaternion.Euler(new Vector3(0, Angle, 0) );
		Vector3 travel = Vector3.forward * Speed * Time.fixedDeltaTime;
		this.transform.position += this.transform.rotation * travel;

		if (!AllowedArea.Contains(this.transform.position))
		{
			// Out of bounds!!!
			transform.LookAt(Vector3.zero);
			Angle = transform.rotation.eulerAngles.y;
		}


	}
}
