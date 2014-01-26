using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MakeFormation : MonoBehaviour {

	[System.Serializable]
	public class FlockRecord
	{
		public string FishTag;
		public int NumFish;
		public FishGuts FishPrefab;

		public void Create(MonoBehaviour mb)
		{
			for (int i = 0; i < NumFish; ++i)
			{
				FishGuts fish = Instantiate(FishPrefab) as FishGuts;
				fish.gameObject.tag = FishTag;
				fish.transform.position = new Vector3(Random.Range(-200,200), Random.Range(-10, 40), Random.Range(-200,200));
			}
		}
	}

	public List<FlockRecord> Flocks = new List<FlockRecord>();

	// Use this for initialization
	void Start () {
		foreach (FlockRecord record in Flocks)
		{
			record.Create(this);
		}

		GameObject eye = new GameObject();
		Formation eye_f = eye.AddComponent<Formation>();
		eye_f.FishTag = "Formation Green";
		eye.transform.parent = this.transform;
		eye.transform.localPosition = Vector3.zero;

		GameObject body = new GameObject();
		Formation body_f = body.AddComponent<Formation>();
		body_f.FishTag = "Formation Blue";
		body.transform.parent = this.transform;
		body.transform.localPosition = Vector3.zero;

		float distance = 7;
		float size = distance;

		// Make the formation spheres:
	
		MakeSphere(body_f, -2*distance, 0, size);

		MakeSphere(body_f, -distance, distance/2, size);
		MakeSphere(body_f, -distance, -distance/2, size);

		MakeSphere(body_f, 0, distance, size);
		MakeSphere(eye_f, 0, 0, size/2 );
		MakeSphere(body_f, 0, -distance, size);

		MakeSphere(body_f, distance, distance*0.5f, size);
		MakeSphere(body_f, distance, distance*1.5f, size);
		MakeSphere(body_f, distance, -distance*0.5f, size);
		MakeSphere(body_f, distance, -distance*1.5f, size);

		MakeSphere(body_f, 2*distance, distance, size);
		MakeSphere(body_f, 2*distance, 0, size/2 );
		MakeSphere(body_f, 2*distance, -distance, size);

//		MakeSphere(body_f, 3*distance, distance/2, size);
//		MakeSphere(body_f, 3*distance, -distance/2, size);
		MakeSphere(body_f, 3*distance, 0, size);

		MakeSphere(body_f, 4*distance, 0, size);

		MakeSphere(body_f, 5*distance, distance, size);
		MakeSphere(body_f, 5*distance, 0, size/2 );
		MakeSphere(body_f, 5*distance, -distance, size);

		MakeSphere(body_f, 6*distance, distance*0.5f, size);
		MakeSphere(body_f, 6*distance, distance*1.5f, size);
		MakeSphere(body_f, 6*distance, -distance*0.5f, size);
		MakeSphere(body_f, 6*distance, -distance*1.5f, size);

	}

	public void MakeSphere(Formation f, float x, float y, float size)
	{
		GameObject g = new GameObject();
		Transform t = g.transform;
		t.parent= f.transform;
		t.localPosition = new Vector3(0,y,-x);
		t.localScale = new Vector3(size,size,size);
	}

}
