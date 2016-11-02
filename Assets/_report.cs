using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class _report : MonoBehaviour {
    public Text lonText;
    public Text latText;
    public Text time;
    // Use this for initialization
    void Start () {
        lonText.text = _SceneComponent.Instance.CenterWorldCoordinates1.longitude.ToString("F4");
        latText.text = _SceneComponent.Instance.CenterWorldCoordinates1.latitude.ToString("F4");
        time.text = "發現時間    " + System.DateTime.Now;
    }
	
	// Update is called once per frame
	void Update () {
	
	}
    public void reportbtnclick()
    {
        SceneManager.LoadScene("POI - Demo scene");
    }
}
