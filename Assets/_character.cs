using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class _character : MonoBehaviour
{
    float time = 0;
    public Text bioname;
    public Image classimage;
    public GameObject Imgpanel;
    public Image bioimage;
    public Text bigbionname;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Bio" && time >= 1f)
        {
            bioname.text = collision.transform.name.Split(';')[0];
            classimage.sprite = Resources.Load(collision.transform.name.Split(';')[1], typeof(Sprite)) as Sprite;
            if (collision.transform.name.Split(';').Length > 2)
            {
                //test.text = collision.transform.name.Split(';')[2];
                //btn.gameObject.SetActive(true);
                bioimage.sprite = _SceneComponent.Instance.DownloadImage(collision.transform.name.Split(';')[2]);
                bigbionname.text = collision.transform.name.Split(';')[0];
            }
            else
            {
                //test.text = "";
                bioimage.sprite = Resources.Load("無照片", typeof(Sprite)) as Sprite;
                bigbionname.text = "";
                //btn.gameObject.SetActive(false);
            }
            time = 0;
        }
        else if (collision.transform.tag == "cafe" && time >= 10f)
        {
            _SceneComponent.Instance.addtreecount();
            time = 0;
        }

        time += Time.deltaTime;
    }

    public void OnBtnShowClick()
    {
        Imgpanel.SetActive(true);
    }
    public void OnBtnCloseClick()
    {
        Imgpanel.SetActive(false);
    }

}
