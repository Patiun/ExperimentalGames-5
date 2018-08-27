using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudBehavior : MonoBehaviour {

    public float range;
    public LayerMask layerMask;
    public Continent continent;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        CheckBelow();
        if(continent != null)
        {
            CheckWin();
        }
	}

    private void CheckBelow()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position,-1*transform.up,out hit, range, layerMask.value))
        {
            continent = hit.collider.gameObject.GetComponent<Continent>();
        } else
        {
            continent = null;
        }
    }

    private void CheckWin()
    {
        if (continent.isAfrica)
        {
            GameController.instance.WinGame();
        }
    }
}
