using UnityEngine;

public class Tilt : MonoBehaviour
{
	void Start()
	{
		Input.gyro.enabled = true;
	}

	void Update()
	{
		Quaternion tilt = Input.gyro.attitude;
		tilt.y = -tilt.y;
		tilt.z = -tilt.z;
		transform.rotation = tilt;
	}
}
