﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class WrapAround : MonoBehaviour {
	public float wrapLeft;
	public float wrapRight;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position.x < wrapLeft) {
			transform.position = new Vector3(wrapRight,transform.position.y,0);
		}
	}
}