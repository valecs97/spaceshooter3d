using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothMovement : MonoBehaviour {

    public float tilt;
    public int smoothness;

    private Rigidbody myBody;
    private Vector3 lastVelocity;

	void Start () {
        myBody = GetComponent<Rigidbody>();
	}
	
	void Update () {

    }

    
}
