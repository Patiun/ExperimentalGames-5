using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terrain : MonoBehaviour {

    public enum TerrainType
    {
        MOUNTAIN,DESERT,HOLYSITE
    };

    public TerrainType terrainType;
    public Continent continent;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public Continent GetContinent()
    {
        return continent;
    }

    public TerrainType GetTerrainType()
    {
        return terrainType;
    }
}
