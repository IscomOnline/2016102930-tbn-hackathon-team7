using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;

public class UserController : MonoBehaviour {

	public GameObject plant_shansu;//要被複製的物件
	public GameObject plant_popu; 
	public GameObject plant_fonshain;//要被複製的物件
	public GameObject plant_mayindan; 
	public Text shansuCountText;
	public Text popuCountText;
	public Text fonshainText;
	public Text mayindanText;
	public Text totalCountText;
	public int totalCount = 1; 
	public Text nameText;
	public GameObject namePanel;
	float time = 0;

	void Awake () 
	{

	}

	void Update (){
		time += Time.deltaTime;
		if (time >= 1) {
			time = 0;
			namePanel.SetActive (false);
		}
		if (totalCount > 4) {
			namePanel.SetActive (true);
		}

		if (Input.GetKey ("down")||Input.GetKey (KeyCode.S)){
			transform.Translate(0.0f,0.0f,20.0f* Time.deltaTime);
		}

		if (Input.GetKey ("up")||Input.GetKey (KeyCode.W)){
			transform.Translate(0.0f,0.0f,20.0f*Time.deltaTime);
		}

		if (Input.GetKey ("left")||Input.GetKey (KeyCode.A)){
			transform.Rotate(0,-180*Time.deltaTime,0);
		}

		if (Input.GetKey ("right")||Input.GetKey (KeyCode.D)){
			transform.Rotate(0,180*Time.deltaTime,0);
		}


		if (Input.GetKeyDown(KeyCode.Alpha2)){
			string countStr = shansuCountText.text;
			int count = int.Parse(countStr.Substring (3, 1));
			if(count !=0){
				count = count - 1;

				if (count > 3) {
					nameText.text = "山蘇花之王";
					GameObject tmp = transform.Find ("GetName").gameObject;
					Debug.Log (tmp.name);
				}
				shansuCountText.text = "數量："+count+"";
				totalCount = totalCount + 1;
				totalCountText.text = totalCount+"";
				Quaternion q = transform.rotation;
				Vector3 pos, pos_c, pos_m, test;
				pos_c = this.transform.FindChild ("Camera").transform.position;
				pos_m = transform.position;
				pos = pos_m + 10 * (pos_m - pos_c);
				test = pos_m - pos_c;
				Instantiate(plant_shansu,new Vector3(pos.x,0.5f,pos.z),transform.rotation);//複製copyGameObject物件(連同該物件身上的腳本一起複製)
				//childGameObject.name = "CopyObject";
			}
		}
		if (Input.GetKeyDown(KeyCode.Alpha1)){
			string countStr = popuCountText.text;
			int count = int.Parse(countStr.Substring (3, 1));
			if(count !=0){
				count = count - 1;
				popuCountText.text = "數量："+count+"";
				totalCount = totalCount + 1;
				totalCountText.text = totalCount+"";
				Quaternion q = transform.rotation;
				Vector3 pos, pos_c, pos_m, test;
				pos_c = this.transform.FindChild ("Camera").transform.position;
				pos_m = transform.position;
				pos = pos_m + 10 * (pos_m - pos_c);
				test = pos_m - pos_c;
				Instantiate(plant_popu,new Vector3(pos.x,0.5f,pos.z),transform.rotation);//複製copyGameObject物件(連同該物件身上的腳本一起複製)
				//childGameObject.name = "CopyObject";
			}
		}
		if (Input.GetKeyDown(KeyCode.Alpha3)){
			string countStr = fonshainText.text;
			int count = int.Parse(countStr.Substring (3, 1));
			if(count !=0){
				count = count - 1;
				fonshainText.text = "數量："+count+"";
				totalCount = totalCount + 1;
				totalCountText.text = totalCount+"";
				Quaternion q = transform.rotation;
				Vector3 pos, pos_c, pos_m, test;
				pos_c = this.transform.FindChild ("Camera").transform.position;
				pos_m = transform.position;
				pos = pos_m + 10 * (pos_m - pos_c);
				test = pos_m - pos_c;
				Instantiate(plant_fonshain,new Vector3(pos.x,0.5f,pos.z),transform.rotation);//複製copyGameObject物件(連同該物件身上的腳本一起複製)
				//childGameObject.name = "CopyObject";
			}
		}
		if (Input.GetKeyDown(KeyCode.Alpha4)){
			string countStr = mayindanText.text;
			int count = int.Parse(countStr.Substring (3, 1));
			if(count !=0){
				count = count - 1;
				mayindanText.text = "數量："+count+"";
				totalCount = totalCount + 1;
				totalCountText.text = totalCount+"";
				Quaternion q = transform.rotation;
				Vector3 pos, pos_c, pos_m, test;
				pos_c = this.transform.FindChild ("Camera").transform.position;
				pos_m = transform.position;
				pos = pos_m + 10 * (pos_m - pos_c);
				test = pos_m - pos_c;
				Instantiate(plant_mayindan,new Vector3(pos.x,0.5f,pos.z),transform.rotation);//複製copyGameObject物件(連同該物件身上的腳本一起複製)
				//childGameObject.name = "CopyObject";
			}
		}
        
    }
    public void OnbtnClick()
    {
        SceneManager.LoadScene("POI - Demo scene");
    }


}
