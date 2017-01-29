using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tool : MonoBehaviour {

    public List<string> affects = new List<string>();

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D coll)
    {
        Debug.Log("Hit: " + coll.name);
        if (coll.GetComponent<InteractableTile>())
        {
            if (affects.Contains(coll.GetComponent<InteractableTile>().type))
            {
                coll.GetComponent<InteractableTile>().Break(transform.parent.gameObject);
            }
        }
    }
}
