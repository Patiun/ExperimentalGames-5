using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestOcean : MonoBehaviour {

    public List<GameObject> continents;

    private float refreshRate = 1f;
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
        foreach(GameObject continent in continents)
        {
            center += continent.transform.position;
        }
        center /= continents.Count;

        transform.position = center;
        nextRefresh = 1 / refreshRate;
    }
}
