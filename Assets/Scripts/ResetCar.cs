using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.N3DS;

public class ResetCar : MonoBehaviour {

	void FixedUpdate () {
		if(Input.GetKeyDown(KeyCode.E) || GamePad.GetButtonTrigger(N3dsButton.L)) 
		{
            GameObject col = GameObject.FindGameObjectWithTag("Player");

            col.transform.position = new Vector3(0, 0, 0);
            col.transform.rotation = new Quaternion(0, 0, 0, 0);

            col.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        }
    }
}
