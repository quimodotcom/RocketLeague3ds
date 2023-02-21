using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioDebugInfo : MonoBehaviour {

	public AudioSource carMixer;

	public void FixedUpdate()
	{
		GetComponent<Text>().text = carMixer.pitch + " ;;; " + carMixer.volume;
	}
}
