using UnityEngine;
using System.Collections;

public class AttributeController : MonoBehaviour {

	public static AttributeController Instance;
	// Use this for initialization
	void Start () {
		this.InstantiateController();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	private void InstantiateController() {
		if(Instance == null)
		{
			Instance = this;
			DontDestroyOnLoad(this);
		}
		else if(this != Instance) {
			Destroy(this.gameObject);
		}
	}
}
