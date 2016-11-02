using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine.UI;

public class _SceneComponent : MonoBehaviour {
    public static _SceneComponent Instance;
    //public Coordinates CenterWorldCoordinates1 = new Coordinates(24.1504536, 120.6832528, 0);
    public Coordinates CenterWorldCoordinates1 = new Coordinates(24.1446, 120.684, 0);
    private Dictionary<string, Texture2D> _imageDic = new Dictionary<string, Texture2D>();
    public int count111=new int() ;
    public int treeCount1 = new int();
    public Text picCount; 
    void Awake()
    {
        this.InstantiateController();
    }

    private void InstantiateController()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else if (this != Instance)
        {
            Destroy(this.gameObject);
        }
    }
    // Use this for initialization
    void Start () {
    }
	
	// Update is called once per frame
	void Update () {
    }

    public Sprite DownloadImage(string pid)
    {
        if (!_imageDic.ContainsKey(pid))
        {
            string url = string.Format("http://api.tbn.org.tw/api/picture?pid={0}&q=25&size=400", pid);
            WWW www = new WWW(url);
            while (!www.isDone) ;
            _imageDic.Add(pid, www.texture);
        }

        return Sprite.Create(_imageDic[pid], new Rect(0,0,_imageDic[pid].width,_imageDic[pid].height), new Vector2(0.5f,0.5f));

    }
    public void addcount()
    {
        count111=count111+1;
    }
    public void addtreecount()
    {
        treeCount1 = treeCount1 + 1;
    }
    //public void GetWebImage(Action<Texture2D> callback, string url)
    //{
    //    if (!_imageDic.ContainsKey(url))
    //        _imageDic.Add(url, null);
    //    if (_imageDic[url] == null)
    //        StartCoroutine(this.DownloadImage((Texture2D image) =>
    //        {
    //            _imageDic[url] = image;
    //            //callback(_imageDic[url]);
    //        }, url));
    //    else
    //        callback(_imageDic[url]);
    //}


}
