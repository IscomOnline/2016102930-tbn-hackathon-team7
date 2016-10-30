using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class _Cylider1 : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.collider.tag);
        if (collision.collider.tag == "Player")
        {
            //SceneManager.LoadScene("memorygame");
        }
    }
}
