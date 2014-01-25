using UnityEngine;
using System.Collections;

public class UserInput : MonoBehaviour {
	public FishGuts fishPrefab;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			Ray mouseClickRay = Camera.main.ScreenPointToRay(Input.mousePosition);
			FishGuts[] allFishes = GameObject.FindObjectsOfType<FishGuts>();
			foreach(FishGuts fish in allFishes){
				fish.handleClick(mouseClickRay);
			}

		//	addFishToWorld(new Vector3(-2,-2,-2));

		}

	}
	/*
 	void addFishToWorld(Vector3 fishPosition){
		FishGuts newFish = (FishGuts)Instantiate(fishPrefab);
		newFish.transform.position = fishPosition;
	}*/

}
