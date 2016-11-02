using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using UnityEngine.SceneManagement;


public class ClickBehavior : MonoBehaviour {

	public Text bioName;
	public Text bioType;
	public Text time;
	public Text lat;
	public Text lon;
	public Image imageTmp;

	public string bioNameStr = "";
	public string bioTypeStr = "";
	public string editorStr = "";
	public System.DateTime timeDatetime;
	public string latStr = "";
	public string lonStr ="";



	// Use this for initialization
	void Start () {
		timeDatetime = System.DateTime.Now;
		time.text = "發現時間    " + timeDatetime;
		lat.text = "25.2";
		lon.text = "125.0";
		//post ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void onClick(){
		bioNameStr = bioName.text;
		bioTypeStr = bioType.text;

		Debug.Log (bioTypeStr + "/" + bioNameStr + "/" + timeDatetime);
        SceneManager.LoadScene("POI - Demo scene");
    }

	public void onSelectImageBtnClick(){


		imageTmp.sprite = Sprite.Create (
			UnityFile.openImage (), new Rect (0, 0, 100, 100), new Vector2 (0.5f,0.5f));


	}


	public void post(){
		NameValueCollection nvcForPost = new NameValueCollection(); //  參數資訊
		nvcForPost.Add("Account", "XXXX"); //登入帳號
		nvcForPost.Add("Password", "XXX");//登入密碼
		nvcForPost.Add("strAddr", "");//地點描述
		nvcForPost.Add("Lon", "121.156");//經度
		nvcForPost.Add("Lat", "24.9455");//緯度
		nvcForPost.Add("SpCName", "谷氨酸棒桿菌");//物種中文學名
		nvcForPost.Add("SpEName", "Corynebacterium glutamicum");//物種英文學名
		nvcForPost.Add("Title", "谷氨酸棒桿菌調查");//標題
		nvcForPost.Add("StartTime", "2015-01-07 07:00:00");//開始時間
		nvcForPost.Add("EndTime", "2015-01-07 17:00:00");//結束時間
		nvcForPost.Add("SurveyContent", "谷氨酸棒桿菌調查內容");//調查描述
		nvcForPost.Add("strCity", "臺北市");//城市
		nvcForPost.Add("strCityTown", "中正區");//鄉鎮區
		nvcForPost.Add("AuthKey", "7d808001-1c1d-4c77-acc4-8955437c4d14");
		string url = "http://api.tbn.org.tw/api/Survey";//api url
		//string fullFileName = @"C:\Users\Public\Pictures\Sample Pictures\Chrysanthemum .jpg";//圖檔完整路徑
		//string contentType = "image/jpeg";//圖檔類型

		//-----------------------------------End參數定義-----------------------------------

		//-----------------------------------建立HttpWebRequest-----------------------------------

		string boundary = "---------------------------" + DateTime.Now.Ticks.ToString("x");//定義邊界
		byte[] boundarybytes = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "\r\n");//轉換為byte array
		HttpWebRequest httpPostRequest = (HttpWebRequest)WebRequest.Create(url);
		httpPostRequest.ContentType = "multipart/form-data; boundary=" + boundary;
		httpPostRequest.Method = "POST";
		httpPostRequest.KeepAlive = true;
		httpPostRequest.Credentials = System.Net.CredentialCache.DefaultCredentials;

		//-----------------------------------request-參數設定-----------------------------------

		Stream requestStream = httpPostRequest.GetRequestStream();
		string formdataTemplate = "Content-Disposition: form-data; name=\"{0}\"\r\n\r\n{1}";
		foreach (string key in nvcForPost.Keys)
		{
			requestStream.Write(boundarybytes, 0, boundarybytes.Length);//定義邊界
			string formItem = string.Format(formdataTemplate, key, nvcForPost[key]);
			byte[] formItemBytes = System.Text.Encoding.UTF8.GetBytes(formItem);
			requestStream.Write(formItemBytes, 0, formItemBytes.Length);//寫入串流
		}
		requestStream.Write(boundarybytes, 0, boundarybytes.Length);//定義邊界

		//-----------------------------------End request-參數設定-----------------------------------

		//-----------------------------------request-檔案標頭檔參數設定----------------------------------- 

		//標頭資訊
		//string headerTemplate = "Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\nContent-Type: {2}\r\n\r\n";
		//string header = string.Format(headerTemplate, "uploadFileName", fullFileName, contentType);
		//byte[] headerBytes = System.Text.Encoding.UTF8.GetBytes(header);
		//requestStream.Write(headerBytes, 0, headerBytes.Length);//寫入串流

		////-----------------------------------End request-檔案標頭檔參數設定----------------------------------- 

		////-----------------------------------request-檔案串流參數設定----------------------------------- 

		//int BUFFER_SIZE = 4096;
		//FileStream fileStream = new FileStream(fullFileName, FileMode.Open, FileAccess.Read);//取得檔案串流
		//byte[] buffer = new byte[BUFFER_SIZE];
		//int bytesRead = 0;
		//while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
		//{
		//    requestStream.Write(buffer, 0, bytesRead);//寫入串流
		//}
		//fileStream.Close();//關閉檔案串流

		//requestStream.Write(boundarybytes, 0, boundarybytes.Length);//定義邊界
		requestStream.Close();//關閉串流

		//-----------------------------------End request-檔案串流參數設定----------------------------------- 

		WebResponse response = null;

		string responseText = "";
		try
		{
			response = httpPostRequest.GetResponse();
			string statusDescriptoin = ((HttpWebResponse)response).StatusDescription;
			int statusCode = (int)((HttpWebResponse)response).StatusCode;

			Stream dataStream = response.GetResponseStream();
			StreamReader reader = new StreamReader(dataStream, Encoding.UTF8);
			responseText = DecodeHtmlChars(reader.ReadToEnd());
			reader.Close();

			if (statusCode == 200)
			{
				System.Diagnostics.Debug.WriteLine("statusDescriptoin = " + statusDescriptoin);//返回狀態資訊
				System.Diagnostics.Debug.WriteLine("responseFromServer = " + responseText);//返回訊息
			}

		}
		catch (WebException e)
		{
			string exceptionMessage = e.ToString();
			System.Diagnostics.Debug.WriteLine(exceptionMessage);
		}
		finally
		{
			if (response != null)
			{
				response.Close();
			}
		}

	}
	public  string DecodeHtmlChars( string source)
	{
		string[] parts = source.Split(new string[] {"&#x"}, StringSplitOptions.None);
		for (int i = 1; i < parts.Length; i++)
		{
			int n = parts[i].IndexOf(';');
			string number = parts[i].Substring(0,n);
			try
			{
				int unicode = Convert.ToInt32(number,16);
				parts[i] = ((char)unicode) + parts[i].Substring(n+1);
			} catch {}
		}
		return String.Join("",parts);
	}

}
