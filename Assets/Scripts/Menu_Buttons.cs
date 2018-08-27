using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu_Buttons : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void StartGame(){
        SceneManager.LoadScene("MainScene");
    }

    public void ViewControls(){
        //SceneManager.LoadScene("ControlsScreen");
    }

    public void QuitGame(){
        Application.Quit();
    }
}
