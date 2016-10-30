using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

namespace GoMap
{
    public class btn_script : MonoBehaviour
    {

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
        public void OnClick()
        {
            SceneManager.LoadScene("POI - Demo scene");
        }
    }
}