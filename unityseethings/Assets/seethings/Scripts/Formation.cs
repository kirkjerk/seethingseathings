using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

public class Formation : MonoBehaviour {

	public float SecondsBetweenTargetUpdates = 0.2f;
	public float NextTargetUpdate = float.MinValue;
	public string FishTag = "Formation Blue";

	Queue<Transform> SpheresRemaining = new Queue<Transform>();
	List<GameObject> FishRemaining = new List<GameObject>();
	int FishPerSphere = 0;

	// Update is called once per frame
	void Update () {

		if (SpheresRemaining.Count > 0)
		{
			// Assign spaces for the next sphere
			Transform sphere = SpheresRemaining.Dequeue();
			FishRemaining.Sort( (a,b) => orderByIncreasingDistance(sphere.position, a,b) );

			foreach( GameObject fish in FishRemaining.Take(FishPerSphere) )
			{
				FishGuts guts = fish.GetComponent<FishGuts>();
				if (guts)
				{
					guts.FormationTarget = sphere;
				}
			}
			
			FishRemaining.RemoveRange(0, FishPerSphere);
			return;
		}

		if (Time.time > NextTargetUpdate)
		{
			NextTargetUpdate = Time.time + SecondsBetweenTargetUpdates;

			GameObject[] fishes = GameObject.FindGameObjectsWithTag(FishTag);
			FishRemaining.Clear();
			FishRemaining.AddRange( fishes );

			FishPerSphere = fishes.Length / transform.childCount;

			List<Transform> children = new List<Transform>();  
			foreach(Transform child in this.transform)
				children.Add(child);

			SpheresRemaining.Clear();
			while(children.Count > 0)
			{
				int index = Random.Range(0,children.Count);
				SpheresRemaining.Enqueue(children[index]);
				children.RemoveAt(index);
			}
		}
	
	}

	int orderByIncreasingDistance(Vector3 reference_point, GameObject a, GameObject b)
	{
		float ma2 = (reference_point - a.transform.position).sqrMagnitude;
		float mb2 = (reference_point - b.transform.position).sqrMagnitude;
		if (ma2 == mb2)
			return 0;
		if (ma2 < mb2)
			return -1;
		return 1;
	}
}
