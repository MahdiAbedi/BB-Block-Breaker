using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour {

    public BallScript ballCtr;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter2D(Collision2D target)
    {
        if (target.gameObject.tag=="Ball")
        {
            ballCtr.myBody.velocity = Vector2.zero;
            //reset the canshoot
            ballCtr.currentBallState = BallScript.BallState.aim;
        }
    }
}
