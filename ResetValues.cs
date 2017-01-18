using UnityEngine;
using System.Collections;

public class ResetValues : MonoBehaviour {

	//calls their reset functions
	void Start () {
		ScoreKeeper.Reset ();
		LifeKeeper.Reset ();
	}
}