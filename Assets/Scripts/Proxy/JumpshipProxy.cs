using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpshipProxy : MonoBehaviour, HealthSetter {

    public HealtBarManager healtBarManager;

    public void SetHealth(float health)
    {
        healtBarManager.Health = health;
    }

    // Use this for initialization
    void Start () {
        Jumpship piece = new Jumpship(this);
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
