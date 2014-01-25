using UnityEngine;

using System.Collections.Generic;

public class MakeWorld : MonoBehaviour {

	public int NUM_FISH_START = 40;
	public float MAX_START_LOC = 200;


	//public FishGuts fishPrefab;

	public List<FishGuts> fishPrefabs;


	// Use this for initialization
	void Start () {

		foreach(FishGuts fishPrefab in fishPrefabs){
			List<FishGuts> fishAdded = new List<FishGuts> ();
			FishGuts firstFish = (FishGuts)Instantiate(fishPrefab);
			fishAdded.Add (firstFish);

			for (int i = 0; i < NUM_FISH_START - 1; i++) {
				FishGuts newFish = (FishGuts)Instantiate(fishPrefab);


				int randomIndex = Random.Range(0,fishAdded.Count);
				newFish.target = fishAdded[randomIndex].transform;  // firstFish.transform;//

				newFish.transform.position = new Vector3(Random.Range(-MAX_START_LOC,MAX_START_LOC),
				                                             Random.Range(-MAX_START_LOC,MAX_START_LOC),
				                                            0);
				fishAdded.Add(newFish);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
