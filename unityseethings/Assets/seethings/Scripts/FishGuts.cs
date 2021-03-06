﻿using UnityEngine;
using System.Collections;




public class FishGuts : MonoBehaviour {

	
	protected float KICKFORCE = 2.0f;
	protected float PERCENTCHANCETOKICK = 30;
	protected float MAXSPEEDTOTRIGGERKICK = 1;

	protected float STRENGTH_OF_SCARE = 20;

	protected float RANDOM_GOAL_BOUNDS = 15;

	public Transform targetTransform = null;
	public Transform FormationTarget = null;
	public FishViz myFishViz;

	//protected Vector3 randomGoal;



	// Use this for initialization
	void Start () {
		myFishViz = this.gameObject.GetComponentInChildren<FishViz> ();
	//	setGoalRandomly ();
	}

	/*void setGoalRandomly(){
			randomGoal = new Vector3 (Random.Range(-RANDOM_GOAL_BOUNDS,RANDOM_GOAL_BOUNDS),
		                          Random.Range(-RANDOM_GOAL_BOUNDS,RANDOM_GOAL_BOUNDS),
		                          Random.Range(-RANDOM_GOAL_BOUNDS,RANDOM_GOAL_BOUNDS));
		}*/

	// Update is called once per frame
	void Update () {
		checkKick ();
	}

	public void setBeacon(BeaconGuts beacon){
		targetTransform = beacon.transform;
	}

	void checkKick(){
		float speed = this.rigidbody.velocity.sqrMagnitude;

		if(speed > MAXSPEEDTOTRIGGERKICK) return;
		//if(dist(x,y,tx,ty) > MINDISTTOTRIGGERKICK){
		if( Random.Range(0f,100.0f) < PERCENTCHANCETOKICK){
			Vector3 deltaFromTarget = getTargetLocation() - this.transform.position;
			Vector3 desiredKick = deltaFromTarget *  KICKFORCE;

			//Debug.Log(desiredKick.magnitude);

			this.rigidbody.AddForce(desiredKick,ForceMode.VelocityChange);
			if(myFishViz) myFishViz.setFacing(desiredKick);
		} 
	}
	
	Vector3 getTargetLocation(){
		if (FormationTarget) {
			// Are we already close enough to the target?
			Vector3 to_target = this.transform.position - FormationTarget.transform.position;
			float d2 = to_target.sqrMagnitude;
			float size2 = FormationTarget.lossyScale.x * FormationTarget.lossyScale.x;
			if (d2 < size2) {
				// We are already inside our target! Yay!  Just bop around a little.
				return this.transform.position + Random.insideUnitSphere * 4;
			}

			// Not inside target!  Try to get there.
			return FormationTarget.position + Random.insideUnitSphere * FormationTarget.lossyScale.x;
		}

		if(targetTransform == null){
			return new Vector3(0,0,0);
		}   else {
			return targetTransform.position;
		}
	}

	public void handleClick (Ray mouseClickRay)
	{
		//Ray mouseClickRay = Camera.main.ScreenPointToRay( Input.mousePosition );
		Vector3 ray_point = FindClosestPointOnRay(mouseClickRay, transform.position);
		Vector3 vectorAwayFromClick = this.transform.position - ray_point;

		vectorAwayFromClick.Normalize ();

		float distanceFromClick = (ray_point - this.transform.position).magnitude;

		//vectorAwayFromClick /= distanceFromClick;
		vectorAwayFromClick *= STRENGTH_OF_SCARE / distanceFromClick;
		this.rigidbody.AddForce(vectorAwayFromClick,ForceMode.VelocityChange);
		if(myFishViz)myFishViz.setFacing(vectorAwayFromClick);

	//	setGoalRandomly ();
	}

	
	Vector3 FindClosestPointOnRay(Ray ray, Vector3 query_point )
	{
		Vector3 to_point_from_origin = query_point - ray.origin;
		Vector3 cross_product = Vector3.Cross(ray.direction, to_point_from_origin);
		float offset_length = cross_product.magnitude / ray.direction.magnitude;
		float theta = Mathf.Asin( offset_length / to_point_from_origin.magnitude );
		float along_ray_to_closest_point = to_point_from_origin.magnitude * Mathf.Cos( theta );
		return ray.origin + (ray.direction * along_ray_to_closest_point);
		
	}
	

}
