using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Playables;

public class LazerAnimation : MonoBehaviour {

    public static LazerAnimation lazerAnimation;

    public bool isAnimating;
    PlayableDirector pd;

	// Use this for initialization
	void Start () {
        lazerAnimation = this;
        pd = GetComponent<PlayableDirector>();
        isAnimating = false;
        beams = new List<GameObject>();
        beamLengths = new List<float>();
	}

    List<GameObject> beams;
    List<float> beamLengths;
    List<Piece> targets;
    Piece attacking;

    public void Animate(Piece attacking, List<Piece> attacked)
    {
        GameObject lazer = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Lazer.prefab");
        beams.Clear();
        beamLengths.Clear();
        targets = attacked;
        this.attacking = attacking;

        foreach (Piece p in attacked)
        {
            Vector3 offset = p.monoBehaviour.transform.position - attacking.monoBehaviour.transform.position;
            Vector3 n = offset;
            n.Normalize();

            double angle = Math.Acos(n.x) * 180.0 / Math.PI;
            if (n.z < 0)
                angle = 360.0 - angle;

            GameObject go = UnityEngine.Object.Instantiate(lazer);
            go.transform.position = attacking.monoBehaviour.transform.position;
            go.transform.Rotate(0, -(float)angle, 0);

            beams.Add(go);
            beamLengths.Add((float)(offset.magnitude - 0.3));
        }

        if (beams.Count == 0)
        {
            foreach (Piece p in targets)
                p.TakeDamage(attacking.damage);
        }
           
        isAnimating = true;
        Game.instance.inAnimation = true;
        transform.position = new Vector3(0, 0, 0);
        pd.Play();
    }

	// Update is called once per frame
	void Update () {
        if (!isAnimating)
            return;
        if (pd.state == PlayState.Paused)
        {
            isAnimating = false;
            Game.instance.inAnimation = false;

            foreach (GameObject beam in beams)
                MonoBehaviour.Destroy(beam);

            foreach (Piece p in targets)
                p.TakeDamage(attacking.damage);

            return;
        }

        for (int i = 0; i < beams.Count; ++i)
        {
            beams[i].transform.GetChild(0).localScale = new Vector3(1, 1, transform.position.x * beamLengths[i]);
        }
	}
}
