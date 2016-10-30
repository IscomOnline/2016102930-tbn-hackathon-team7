using UnityEngine;
using System.Collections;

public class _pauseBtn : MonoBehaviour {

    public GameObject buttonpanel;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void OnPauseClick()
    {
        if (buttonpanel.gameObject.active)
            buttonpanel.gameObject.SetActive(false);
        else
            buttonpanel.gameObject.SetActive(true);
    }
}
