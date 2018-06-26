using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealtBarManager : MonoBehaviour {
    public float Health;

    Transform GreenCylinder;
    Transform RedCylinder;

	// Use this for initialization
	void Start () {
        GreenCylinder = transform.GetChild(0);
        RedCylinder = transform.GetChild(1);
	}
	
	// Update is called once per frame
	void Update () {
        GreenCylinder.transform.localScale = new Vector3(1, Health * 2, 1);
        RedCylinder.transform.localScale = new Vector3(1, 2 - Health * 2, 1);
	}
}
