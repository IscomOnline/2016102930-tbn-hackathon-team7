using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    private GameObject backgroundObj, temp, first, second;
    private List<GameObject> tiles = new List<GameObject>();
    private List<int> values = new List<int>();
    private List<int> valuesTwo = new List<int>();
    private Ray ray;
    private RaycastHit hit;
    private bool error = false, solved = false, allow = false;
    private float startTime, endTime;
    private int matches = 0, moves = 0;

    public GUISkin memoSkin;
    public GameObject tile, background;
    public Texture[] images;
    public float spacing, pause;
    public int horizontal, vertical;
	[SerializeField]
	private Text text0;


	void Start () {
        // Check if the product of horizontal and vertical variables is even number. If not, stop.
        if ((horizontal * vertical) % 2 != 0)
        {
            Debug.Log("Incoorrect number of tiles!");
            error = true;
            return;
        }
        // Check if we have enough images. If not, stop.
        if (images.Length < (horizontal * vertical) / 2)
        {
            Debug.Log("Not enough images! " + (((horizontal * vertical) / 2) - images.Length).ToString() + " image(s) missing.");
            error = true;
            return;
        }
        // Check spacing variable. It's used to define spacing between tiles. It should not be below 1.
        if (spacing < 1)
        {
            spacing = 1;
        }

        // Instantiate background plane
        InstantiateBackground(0.2f);

        //Instantiate tiles
        for (int i = 0; i < horizontal; i++)
        {
            for (int j = 0; j < vertical; j++)
            {
                temp = Instantiate(tile, new Vector3(i * 10 * tile.transform.localScale.x * spacing, 10, j * 10 * tile.transform.localScale.z * spacing), Quaternion.identity) as GameObject;
                temp.name = "tile_" + i.ToString() + "_" + j.ToString();
                temp.transform.eulerAngles = new Vector3(180, 0, 0);
                temp.transform.parent = backgroundObj.transform;
                tiles.Add(temp);
			}
		}

        // Center the background and tiles to the middle of the screen
        CenterBackground();

        // Create matching lists
        for (int i = 0; i < (horizontal * vertical) / 2; i++)
        {
            values.Add(i);
            valuesTwo.Add(i);
        }
        // Randomize them...
        Shuffle(values);
        Shuffle(valuesTwo);

        // Assign values from two lists to the tiles.
        // This values define image assigned to the tiles. 
        for (int i = 0; i < tiles.Count; i++)
        {
            if (i < tiles.Count / 2)            // assign images to the first half of the tiles
            {
                tiles[i].GetComponent<Pad>().value = values[i];
                tiles[i].GetComponent<Pad>().front.GetComponent<Renderer>().material.mainTexture = images[values[i]];
            }
            else                               // assign images to the  second half of the tiles
            {
                tiles[i].GetComponent<Pad>().value = valuesTwo[i - (tiles.Count / 2)];
                tiles[i].GetComponent<Pad>().front.GetComponent<Renderer>().material.mainTexture = images[valuesTwo[i - (tiles.Count / 2)]];
            }
        }

        // We are ready! Set start time.
        startTime = Time.time;

        // Allow clicking on tiles. Control element.
        allow = true;
	}

    // Randomize values in list
    private void Shuffle(List<int> numbers)
    {
        for (int t = 0; t < numbers.Count; t++)
        {
            int tmp = numbers[t];
            int r = UnityEngine.Random.Range(t, numbers.Count);
            numbers[t] = numbers[r];
            numbers[r] = tmp;
        }
    }

    void Update()
    {
        if (error) return;

        if (solved) return;

        if (Input.GetMouseButtonDown(0) && allow)
        {
            // Check if we can open new tile.
            if (second != null) return;
            // Raycasting
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                // open tile
                hit.transform.eulerAngles = new Vector3(0, 0, 0);
                // 
                if (first == null)
                {
                    first = hit.transform.gameObject;
                }
                else
                {
                    // Just to be save that we don't compare same tile
                    if (hit.transform.gameObject == first)
                    {
                        first.transform.eulerAngles = new Vector3(180, 0, 0);
                        return;
                    }

                    // if it's ok...
                    second = hit.transform.gameObject;
                    allow = false;

                    // Check if two tiles are matching
                    StartCoroutine(CheckMatch());
                }
            }
        }
    }

    void OnGUI()
    {
		GUIStyle style = new GUIStyle ();
		style.fontSize = 24;
		style.fontStyle = FontStyle.Bold;
		style.alignment = TextAnchor.MiddleCenter;

        if (GUI.skin != memoSkin)
            GUI.skin = memoSkin;

        // Show message the the memory game is solved and time 
        if (solved)
        {
			
			text0.text = "竟然打敗我！ 圖鑑就讓你 拿去吧～！";



			GUI.Box(new Rect((Screen.width / 2) , (Screen.height / 2) , 600, 600), "Solved!",style);
			GUI.Label(new Rect(10, 10, 90, 30), "Time",style);
			GUI.Label(new Rect(100, 10, 200, 30), FinalTime(),style);
        }
        else
        {
            // Show time since game started
			GUI.Label(new Rect(10, 10, 90, 30), "Time",style);
			GUI.Label(new Rect(100, 10, 200, 30), CurrentTime(),style);
        }

        // Show number of moves
		GUI.Label(new Rect(10, 40, 90, 50), "Moves",style);
		GUI.Label(new Rect(100, 40, 200, 30), moves.ToString(),style);
    }

    // Returns time since game started
    private string CurrentTime()
    {
        TimeSpan t = TimeSpan.FromSeconds(Mathf.RoundToInt(Time.time - startTime));
        return String.Format("{0:D2}:{1:D2}", t.Minutes, t.Seconds);
    }

    // Returns game finish time
    private string FinalTime()
    {
        TimeSpan t = TimeSpan.FromSeconds(Mathf.RoundToInt(endTime - startTime));
        return String.Format("{0:D2}:{1:D2}", t.Minutes, t.Seconds);
    }

    // Check if two tiles are mathing
    IEnumerator CheckMatch()
    {
        // Wait for defined pause
        yield return new WaitForSeconds(pause);

        // checking...
        if (first.GetComponent<Pad>().value == second.GetComponent<Pad>().value)
        {
            // we have match, count it...
            matches++;
            Destroy(first);
            Destroy(second);
        }
        else
        {
            first.transform.eulerAngles = new Vector3(180, 0, 0);
            second.transform.eulerAngles = new Vector3(180, 0, 0);
        }

        // reset variables for next comparation
        first = null;
        second = null;

        // count moves
        moves++;

        // if we have max. number of matches, game is over
        if (matches == ((horizontal * vertical) / 2))
        {
            solved = true;
            endTime = Time.time;
        }

        allow = true;
    }

    // Center the whole memory game
    private void CenterBackground()
    {
        backgroundObj.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width/2, Screen.height/2, 100));
    }

    private void InstantiateBackground(float scale)
    {
        if (horizontal % 2 != 0 && vertical % 2 == 0)
            backgroundObj = Instantiate(background, new Vector3((10 * tile.transform.localScale.x * spacing) * (horizontal / 2), 10, (10 * tile.transform.localScale.z * spacing) * (vertical / 2) - (5 * tile.transform.localScale.z * spacing)), Quaternion.identity) as GameObject;
        else if (horizontal % 2 == 0 && vertical % 2 != 0)
            backgroundObj = Instantiate(background, new Vector3((10 * tile.transform.localScale.x * spacing) * (horizontal / 2) - (5 * tile.transform.localScale.x * spacing), 10, (10 * tile.transform.localScale.z * spacing) * (vertical / 2)), Quaternion.identity) as GameObject;
        else if (horizontal % 2 == 0 && vertical % 2 == 0)
            backgroundObj = Instantiate(background, new Vector3((10 * tile.transform.localScale.x * spacing) * (horizontal / 2) - (5 * tile.transform.localScale.x * spacing), 10, (10 * tile.transform.localScale.z * spacing) * (vertical / 2) - (5 * tile.transform.localScale.z * spacing)), Quaternion.identity) as GameObject;
       
        backgroundObj.transform.eulerAngles = new Vector3(180, 0, 0);

        if (scale < 0) scale = 0;

        backgroundObj.transform.localScale = new Vector3(((tile.transform.localScale.x * spacing) * horizontal) + scale, 1, ((tile.transform.localScale.x * spacing) * vertical) + scale);
    }
}
