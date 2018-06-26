using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class MovementAnimation : MonoBehaviour {

    public static MovementAnimation movementAnimation;

    PlayableDirector pd;

    Vector3 initialPosition;
    Vector3 target;
    bool isAnimating;
    Piece pieceToAnimate;

    public bool IsAnimating()
    {
        return isAnimating;
    }

    public void AnimateMove(Piece piece, Vector3 location, Vector3 dest)
    {
        transform.position = new Vector3(0, 0, 0);

        initialPosition = piece.monoBehaviour.transform.position;
        target = dest;
        pieceToAnimate = piece;

        isAnimating = true;
        pd.Play();
    }

	// Use this for initialization
	void Start () {
        pd = GetComponent<PlayableDirector>();
        movementAnimation = this;
        isAnimating = false;
	}

	// Update is called once per frame
	void Update () {
        if (!isAnimating)
            return;

        if (pd.state == PlayState.Paused)
        {
            isAnimating = false;
            pieceToAnimate.monoBehaviour.transform.position = target;
            return;
        }

        Vector3 tmp = (1 - transform.position.x) * initialPosition + transform.position.x * target;
        tmp.y = transform.position.y;
        pieceToAnimate.monoBehaviour.transform.position = tmp;
	}
}
