using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadMatch : MonoBehaviour {

    public GameObject[] cars;

    public static LoadMatch instance;

    void Start()
    {
        instance = this;
    }

    public int index;

    public void Select (int index)
	{
        this.index = index;

        foreach (Transform t in FindObjectsOfType<Transform>())
        {
            Destroy(t.gameObject);
        }

        SceneManager.LoadScene("Main", LoadSceneMode.Additive);
    }

    public void LoadYourAsyncScene()
    {
        // The Application loads the Scene in the background as the current Scene runs.
        // This is particularly good for creating loading screens.
        // You could also load the Scene by using sceneBuildIndex. In this case Scene2 has
        // a sceneBuildIndex of 1 as shown in Build Settings.

        GameObject spawn = GameObject.Find("PlayerSpawn");

        var clone = Instantiate(cars[index]);
    
        clone.transform.position = spawn.transform.position;
        clone.transform.rotation = spawn.transform.rotation;

        FindObjectOfType<CameraFollow>().target = clone.transform;
    }
}
