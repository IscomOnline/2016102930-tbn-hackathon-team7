using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class btn_controller : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnbtnClick()
    {
        _SceneComponent.Instance.addcount();
        SceneManager.LoadScene("POI - Demo scene");
    }
    public void OnGamebtnClick()
    {
        
        SceneManager.LoadScene("Demo_3");
        //_SceneComponent.Instance.addcount();

    }
    public void OnGamebtn2Click()
    {
        
        SceneManager.LoadScene("memorygame");
        //_SceneComponent.Instance.addcount();

    }
    public void OnUploadClick()
    {
        SceneManager.LoadScene("BioUpload");
    }
    public void OnReportClick()
    {
        //#####
        SceneManager.LoadScene("report");
    }
    public void OnAboutClick()
    {
        //#####
        SceneManager.LoadScene("BioUpload");
    }
}
