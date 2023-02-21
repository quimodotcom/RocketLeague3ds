using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.N3DS;

public class BoostController : MonoBehaviour {

	[SerializeField] private float boostPower;

	[SerializeField] private ParticleSystem[] tailPipeParticles;

	private bool boosting;

    void FixedUpdate () {
		boosting = Input.GetButton("Jump") || GamePad.GetButtonHold(N3dsButton.R);

		if (boosting)
		{
			GetComponent<Rigidbody>().AddForce(transform.forward * boostPower);

			foreach (var particle in tailPipeParticles)
			{
				particle.Play();
			}
		}
	}
}