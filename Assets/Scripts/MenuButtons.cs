using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuButtons : MonoBehaviour {

    Button button;

	// Use this for initialization
	void Start () {
        button = GetComponent<Button>();
        button.onClick.AddListener(Execute);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void Execute() {
        if (button.name.Equals("Start"))
        {
            SceneManager.LoadScene(1);
        }
        else if (button.name.Equals("Quit"))
        {
            Application.Quit();
        }
        else if (button.name.Equals("Credits")) {
            SceneManager.LoadScene(2);
        }
        else if (button.name.Equals("Back")) {
            SceneManager.LoadScene(0);
        }
    }
}
