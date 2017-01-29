using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour {

    public float speed = .05f;
    public Rigidbody2D rb;

    public GameObject tool;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
	}

    void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Hold hoe
            tool.GetComponent<Animator>().enabled = true;
            tool.GetComponent<BoxCollider2D>().enabled = true;

        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            // Release Hoe
            tool.GetComponent<Animator>().enabled = false;
            tool.GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    void FixedUpdate()
    {
        float movex = Input.GetAxis("Horizontal");
        float movey = Input.GetAxis("Vertical");
        rb.velocity = new Vector2(movex * speed, movey * speed);
    }
}
