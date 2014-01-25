using UnityEngine;
using System.Collections;




public class FishGuts : MonoBehaviour {

	
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

	public void handleClick (Ray mouseClickRay)
	{
		//Ray mouseClickRay = Camera.main.ScreenPointToRay( Input.mousePosition );
		Vector3 ray_point = FindClosestPointOnRay(mouseClickRay, transform.position);
		Vector3 vectorAwayFromClick = this.transform.position - ray_point;
		this.rigidbody.AddForce(vectorAwayFromClick,ForceMode.VelocityChange);
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
