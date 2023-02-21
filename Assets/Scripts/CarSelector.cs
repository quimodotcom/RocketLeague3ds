using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.N3DS;

public class CarSelector : MonoBehaviour {

	public List<GameObject> carPrefabs;

	public CameraFollow bugFix;

    int arrayPos = 1;

    public void FixedUpdate()
    {
        if(GamePad.GetButtonTrigger(N3dsButton.X))
        {
            ChangeCarDEV();
        }
    }

    public void ChangeCarDEV()
	{
		var player = GameObject.FindGameObjectWithTag("Player");

        if (arrayPos >= carPrefabs.Count - 1)
        {
            arrayPos = 0;
        }
        else
        {
            arrayPos += 1;
        }

        var clone = Instantiate(carPrefabs[arrayPos]);
        clone.transform.position = player.transform.position;
        clone.transform.rotation = player.transform.rotation;

        bugFix.target = clone.transform;

        Destroy(player);
    }
}
