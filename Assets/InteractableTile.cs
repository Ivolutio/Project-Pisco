using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableTile : MonoBehaviour {

    public string type;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public virtual void Break(GameObject source)
    {
        Debug.Log("Break");
    }
}
