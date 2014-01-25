using UnityEngine;
using System.Collections;




public class FishMove : MonoBehaviour {

	
	public float KICKFORCE = .04f;
	public float PERCENTCHANCETOKICK = 10;
	public float MAXSPEEDTOTRIGGERKICK = 1;
	
	public Transform target = null;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		checkKick ();
	}

	void checkKick(){
		float speed = this.rigidbody.velocity.sqrMagnitude;

		if(speed > MAXSPEEDTOTRIGGERKICK) return;
		//if(dist(x,y,tx,ty) > MINDISTTOTRIGGERKICK){
		if( Random.Range(0f,100.0f) < PERCENTCHANCETOKICK){
			Vector3 deltaFromTarget = getTargetLocation() - this.transform.position;
			Vector3 desiredKick = deltaFromTarget *  KICKFORCE;
			this.rigidbody.AddForce(desiredKick,ForceMode.VelocityChange);
		} 
	}
	
	Vector3 getTargetLocation(){
		if(target == null){
			return Vector3.zero;
		}   else {
			return target.position;
		}
	}
}
