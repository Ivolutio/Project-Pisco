using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour {

    public float speed = .05f;

    public Rigidbody2D rb;

	// Use this for initialization
	void Start () {
		
	}


    void FixedUpdate()
    {
        float movex = Input.GetAxis("Horizontal");
        float movey = Input.GetAxis("Vertical");
        rb.velocity = new Vector2(movex * speed, movey * speed);
    }
}
