using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GlobalVariables : MonoBehaviour {

    public Text gasCounter;


    public BuildingSpawner BuildingSpawner;
    public GameObject LocalPlayer;

    public static GlobalVariables variables;

	// Use this for initialization
	void Awake () {
        variables = this;
	}

}
