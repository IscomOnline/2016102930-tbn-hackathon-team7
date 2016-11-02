using UnityEngine;
using System.Collections;
using System;

namespace GoMap
{
    public class Flag_controler : MonoBehaviour
    {
        string[] list_url = new string[4]
            {
            "http://taibnet.sinica.edu.tw/uploads_moved/20120211032103_347278.jpg",
            "http://www.tbn.org.tw/Public/UploadSurveyImagesFiles/585436/O/6d997777-e1fa-4f73-afd1-39786c4a41ad.jpg",
            "http://api.tbn.org.tw/api/picture?q=25&size=600&pid=SPIC-44782",
            "http://api.tbn.org.tw/api/picture?q=25&size=600&pid=SPIC-1029915"
            };
        string url;
        // Use this for initialization
        void Start()
        {
            url = list_url[LocationManager.Instance.r.Next(0, 3)];
            this.GetWebImage(OnWebImage, url);
        }

        // Update is called once per frame
        void Update()
        {

        }
        #region GetWebImg
        public IEnumerator DownloadImage(Action<Texture2D> callback, string url)
        {
            WWW www = new WWW(url);
            yield return www;
            callback(www.texture);
        }
        public void GetWebImage(Action<Texture2D> callback, string url)
        {
            if (!LocationManager.Instance._ImageDic.ContainsKey(url))
                LocationManager.Instance._ImageDic.Add(url, null);
            if (LocationManager.Instance._ImageDic[url] == null)
            {
                LocationManager.Instance.loadcount++;
                StartCoroutine(this.DownloadImage((Texture2D image) =>
                {
                    LocationManager.Instance._ImageDic[url] = image;
                    callback(LocationManager.Instance._ImageDic[url]);
                }, url));

            }
            else
                callback(LocationManager.Instance._ImageDic[url]);
        }
        private void OnWebImage(Texture2D image)
        {
            //Renderer renderer = renderObj.GetComponent<Renderer>();
            //renderer.material.mainTexture = LocationManager.Instance.www.texture;
            transform.Find("Cylinder (1)").GetComponent<Renderer>().material.mainTexture = image;
            transform.Find("Cylinder (2)").GetComponent<Renderer>().material.mainTexture = image;
        }
        #endregion
    }
}
