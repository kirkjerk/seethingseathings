using UnityEngine;

using System.Collections.Generic;

public class MakeWorld : MonoBehaviour {

	public int NUM_FISH_START = 40;
	public float MAX_START_LOC = 200;

	public FishMove fishPrefab;

	// Use this for initialization
	void Start () {

		List<FishMove> fishAdded = new List<FishMove> ();

		FishMove firstFish = (FishMove)Instantiate(fishPrefab);
		fishAdded.Add (firstFish);

		for (int i = 0; i < NUM_FISH_START - 1; i++) {
			FishMove newFish = (FishMove)Instantiate(fishPrefab);


			int randomIndex = Random.Range(0,fishAdded.Count);
			newFish.target =  fishAdded[randomIndex].transform;

			newFish.transform.position = new Vector3(Random.Range(-MAX_START_LOC,MAX_START_LOC),
			                                             Random.Range(-MAX_START_LOC,MAX_START_LOC),
			                                             Random.Range(-MAX_START_LOC,MAX_START_LOC));
			fishAdded.Add(newFish);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
