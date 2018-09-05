using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudBehavior : MonoBehaviour {

    public float range;
    public LayerMask layerMask;
    public Continent continent;
    public Terrain terrain;
    public float maxWater, curWater, waterAbsorptionRate, waterRainRate;
    public float rainThreshold;
    public bool canRain, isRaining, isBlessed;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        CheckBelow();
        CheckTerrain();
        if (continent != null)
        {
            CheckWin();
        }

        if (isRaining)
        {
            curWater -= Time.deltaTime * waterRainRate;
        }

        if (curWater >= maxWater * rainThreshold)
        {
            canRain = true;
        }
        else if (curWater <= 0)
        {
            canRain = false;
            isRaining = false;
        } else if (curWater >= maxWater)
        {
            isRaining = true;
        }

	}

    private void CheckBelow()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position,-1*transform.up,out hit, range, layerMask.value))
        {
            continent = hit.collider.gameObject.GetComponent<Continent>();
            terrain = hit.collider.gameObject.GetComponent<Terrain>();
            if (terrain != null)
            {
                continent = terrain.GetContinent();
            }
            
        } else
        {
            continent = null;
        }
    }

    private void CheckWin()
    {
        if (continent.isAfrica && isBlessed && (canRain || isRaining))
        {
            GameController.instance.WinGame();
        }
    }

    private void Bless()
    {
        isBlessed = true;
    }

    private void CheckTerrain()
    {
        //Over Ocean
        if (continent == null)
        {
            curWater += Time.deltaTime * waterAbsorptionRate;
        }else
        {
            if (terrain != null)
            {
                switch (terrain.GetTerrainType())
                {
                    case Terrain.TerrainType.MOUNTAIN:
                        if (canRain)
                        {
                            isRaining = true;
                            canRain = false;
                        }
                        break;
                    case Terrain.TerrainType.DESERT:
                        curWater -= Time.deltaTime * waterAbsorptionRate;
                        break;
                    case Terrain.TerrainType.HOLYSITE:
                        Bless();
                        break;
                }
            }
        }
        if (curWater > maxWater) { curWater = maxWater; }
        if (curWater < 0) { curWater = 0; }
    }
}
