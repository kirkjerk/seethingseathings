using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CoralMaker : MonoBehaviour {

	public Transform CoralPiecePrefab;
	public int MaxTiers = 20;
	public float ForkProbability = 0.1f;
	public float EndProbability = 0.1f;
	public Vector3 MaxNextTierOffset = new Vector3(1,1,1);
	public int MaxPieces = 40;

	protected int TiersCreated = 0;

	Queue<Transform> PiecesToExpandNow = new Queue<Transform>();
	Queue<Transform> PiecesToExpandNext = new Queue<Transform>();

	// Use this for initialization
	void Start () {
		AddPiece(this.transform);
		++TiersCreated;
	}
	
	// Update is called once per frame
	void Update () {
		if (PiecesToExpandNow.Count > 0)
		{
			Transform expand_me = PiecesToExpandNow.Dequeue();
			Expand(expand_me);
		}
		else
		{
			++TiersCreated;
			PiecesToExpandNow = PiecesToExpandNext;
			PiecesToExpandNext = new Queue<Transform>();
		}
	}

	void Expand (Transform expand_me)
	{
		if (Random.value < EndProbability)
			return;

		AddPiece(expand_me);

		if (Random.value < ForkProbability)
		{
			AddPiece(expand_me);
		}
	}

	void AddPiece(Transform expand_me)
	{
		if (MaxPieces <= 0)
			return;
		--MaxPieces;

		Quaternion rotation = Quaternion.Euler(0,Random.Range(0.0f, 360.0f),0);
		Vector3 offset = rotation * MaxNextTierOffset;
		Vector3 new_position = expand_me.position + offset;

		Transform next = Instantiate(CoralPiecePrefab) as Transform;
		next.position = new_position;
		next.parent = this.transform;
		PiecesToExpandNext.Enqueue(next);
	}
}
