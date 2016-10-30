using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
    public GameObject panel;
    // Use this for initialization
    
	void Start () {
        panel.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
    int i = 0;
    public void onClick()
    {
       panel.SetActive(true);
    }
}
