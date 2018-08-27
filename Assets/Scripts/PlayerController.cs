using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    
    public float speed = 0.1F;
    public GameObject spaceText, cloudGameObject;
    Vector3 movementVector = Vector3.zero;

	// Use this for initialization
	void Start () {
		
	}
	
    void Update()
    {
        movementVector = Vector3.zero;

        if(Input.GetKeyDown(KeyCode.Space)){
            spaceText.SetActive(true);
            StartCoroutine(NoMoreSpace());
        }
        if(Input.GetKey(KeyCode.W)){
            movementVector += Vector3.up * speed;
        }
        if (Input.GetKey(KeyCode.A)){
            movementVector += Vector3.left * speed;
        }
        if (Input.GetKey(KeyCode.S))
        {
            movementVector += Vector3.down * speed;
        }
        if (Input.GetKey(KeyCode.D))
        {
            movementVector += Vector3.right * speed;
        }

        cloudGameObject.transform.Translate(movementVector);
    }

    IEnumerator NoMoreSpace(){
        yield return new WaitForSeconds(1.0f);
        spaceText.SetActive(false);
    }
}
