using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

public class Formation : MonoBehaviour {

	public float SecondsBetweenTargetUpdates = 0.2f;
	public float NextTargetUpdate = float.MinValue;
	public string FishTag = "Formation Blue";

	// Update is called once per frame
	void Update () {
		if (Time.time > NextTargetUpdate)
		{
			NextTargetUpdate = Time.time + SecondsBetweenTargetUpdates;

			GameObject[] fishes = GameObject.FindGameObjectsWithTag(FishTag);
			List<GameObject> remaining_fish = new List<GameObject>(fishes);

			int fish_per_child = fishes.Length / transform.childCount;

			List<Transform> children = new List<Transform>();  
			foreach(Transform child in this.transform)
				children.Add(child);
			List<Transform> randomized_children = new List<Transform>();
			while(children.Count > 0)
			{
				int index = Random.Range(0,children.Count);
				randomized_children.Add(children[index]);
				children.RemoveAt(index);
			}

			foreach (Transform child in randomized_children)
			{
				// Want the N closest fish
				remaining_fish.Sort( (a,b) => orderByIncreasingDistance(child.position, a,b) );

				foreach( GameObject fish in remaining_fish.Take(fish_per_child) )
				{
					FishGuts guts = fish.GetComponent<FishGuts>();
					if (guts)
					{
						guts.FormationTarget = child;
					}
				}

				remaining_fish.RemoveRange(0, fish_per_child);
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
