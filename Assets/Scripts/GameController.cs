using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Vuforia;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
    public static GameController instance;

    public float time, timeRemaining;
    public GameObject cloud, ocean;
    public bool ready,gameOver;
    public List<GameObject> continents;
    public GameObject winPanel,losePanel;
    public Text timeText;

    private readonly float cloudHeight = 150f;

	// Use this for initialization
	void Start () {
        instance = this;
        timeRemaining = time;
	}
	
	// Update is called once per frame
	void Update () {
        DebugControls();
        if (!gameOver)
        {
            if (!ready)
            {
                CheckContinents();
            }
            else
            {
                timeRemaining -= Time.deltaTime;
                if (timeRemaining > 0)
                {
                    RepresentTime();
                }
                else
                {
                    LoseGame();
                }
            }
        }
	}

    private void DebugControls()
    {
        if (Input.GetKeyDown("l"))
        {
            SceneManager.LoadScene(0);
        }
    }

    private void RepresentTime()
    {
        float minutes = Mathf.Floor(timeRemaining / 60);
        float seconds = Mathf.RoundToInt(timeRemaining % 60);
        string minutesStr = minutes.ToString();
        string secondsStr = seconds.ToString();

        if (minutes < 10)
        {
            minutesStr = "0" + minutes.ToString();
        }
        if (seconds < 10)
        {
            secondsStr = "0" + Mathf.RoundToInt(seconds).ToString();
        }
        timeText.text = minutesStr + ":" + secondsStr;
    }

    public void CheckContinents()
    {
        List<GameObject> spawnable = new List<GameObject>();
        foreach(GameObject continent in continents)
        {
            if (continent.GetComponent<TrackableBehaviour>().CurrentStatus != TrackableBehaviour.Status.TRACKED)
            {
                return;
            }
            if (!continent.GetComponentInChildren<Continent>().isAfrica)
            {
                spawnable.Add(continent);
            }
        }
        ready = true;
        StartGame(spawnable);
    }

    public void StartGame(List<GameObject> spawnablePos)
    {
        Vector3 pos = spawnablePos[Random.Range(0, spawnablePos.Count)].transform.position;
        pos.y = cloudHeight;
        cloud.SetActive(true);
        cloud.transform.position = pos;
        ocean.SetActive(true);
    }

    public void WinGame()
    {
        winPanel.SetActive(true);
    }

    public void LoseGame()
    {
        timeText.text = "00:00";
        losePanel.SetActive(true);
    }
}
