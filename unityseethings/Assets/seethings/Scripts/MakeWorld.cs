using UnityEngine;

using System.Collections.Generic;

public class MakeWorld : MonoBehaviour {

	public int NUM_FISH_START = 40;
	public float MAX_START_LOC = 200;


	//public FishGuts fishPrefab;

	public List<FishGuts> fishPrefabs;
	public BeaconGuts beaconPrefab;

	protected List<BeaconGuts> beacons = new List<BeaconGuts>();

	// Use this for initialization
	void Start () {

		foreach(FishGuts fishPrefab in fishPrefabs){
			List<FishGuts> fishAdded = new List<FishGuts> ();
			FishGuts firstFish = (FishGuts)Instantiate(fishPrefab);

			BeaconGuts beacon = (BeaconGuts)Instantiate(beaconPrefab);

			beacon.transform.position = randomPosition();

			fishAdded.Add (firstFish);

			firstFish.setBeacon(beacon);
			beacons.Add(beacon);

			for (int i = 0; i < NUM_FISH_START - 1; i++) {
				FishGuts newFish = (FishGuts)Instantiate(fishPrefab);


				int randomIndex = Random.Range(0,fishAdded.Count);
				newFish.targetTransform = fishAdded[randomIndex].transform;  // firstFish.transform;//

				newFish.transform.position = randomPosition();
				fishAdded.Add(newFish);
			}
		}
	}

	Vector3 randomPosition(){
		return new Vector3(Random.Range(-MAX_START_LOC,MAX_START_LOC),
		            Random.Range(-MAX_START_LOC,MAX_START_LOC),
		            Random.Range(-MAX_START_LOC,MAX_START_LOC));
	}

	protected string numKeys = "1234567890";

	// Update is called once per frame
	void Update () {
		for(var i = 0; i < numKeys.Length && i < beacons.Count; i++){
			if(Input.GetKeyDown(numKeys.Substring(i,1))){
				BeaconGuts beacon = beacons[i];
				beacon.transform.position = Camera.main.transform.position;

			}
			if(Input.GetKey(numKeys.Substring(i,1))){
				BeaconGuts beacon = beacons[i];
				beacon.transform.position += Camera.main.transform.rotation * Vector3.forward;

			}
		}

	}
}
