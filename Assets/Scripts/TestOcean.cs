using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class TestOcean : MonoBehaviour {

    public List<GameObject> continents;

    private float refreshRate = 60f;
    private float nextRefresh;

	// Use this for initialization
	void Start () {
        nextRefresh = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time > nextRefresh)
        {
            RefreshPosition();
        }
	}

    public void RefreshPosition()
    {
        Vector3 center = Vector3.zero;
        Vector3 angles = Vector3.zero;
        int count = 0;
        foreach(GameObject continent in continents)
        {
            if (continent.GetComponent<TrackableBehaviour>().CurrentStatus != TrackableBehaviour.Status.TRACKED)
            {
                center += continent.transform.position;
                angles += continent.transform.rotation.eulerAngles;
                count++;
            }
        }
        if (count > 0)
        {
            center /= count;
            angles /= count;
        }

        transform.position = center;
        //transform.rotation = Quaternion.Euler(angles);
        nextRefresh = 1 / refreshRate;
    }
}
