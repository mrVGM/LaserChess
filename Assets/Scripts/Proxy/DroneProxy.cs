﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneProxy : MonoBehaviour, HealthSetter {

    public HealtBarManager healtBarManager;

    public float Health
    {
        set
        {
            healtBarManager.Health = value;
        }
    }
    
    // Use this for initialization
    void Start () {
        Drone piece = new Drone(this);
        piece.x = System.Convert.ToInt32(transform.position.x + 3.5);
        piece.y = System.Convert.ToInt32(transform.position.z + 3.5);

        Game.instance.pieces[piece.x, piece.y] = piece;

        Transform healthBar = transform.Find("HealthBar");
        healtBarManager = healthBar.GetComponent<HealtBarManager>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}