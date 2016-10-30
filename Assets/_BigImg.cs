using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class _BigImg : MonoBehaviour {
	float time=0;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		time += Time.deltaTime;
		if (time >= 1) {
			time = 0;
			gameObject.SetActive (false);
		}
	}
}
