using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;

public class NativePluginExamples : MonoBehaviour
{
	static string[] messages = new string[] {
		"Good morning.",
		"Hello.",
		"Good evening.",
		"Good night.",
	};


	[DllImport ("__Internal")]
	private static extern void ShowApplet (string message);

	void OnGUI ()
	{
		foreach (var message in messages)
		{
			if (GUILayout.Button (message))
			{
#if UNITY_N3DS && !UNITY_EDITOR
				ShowApplet (message);
#endif
			}
		}
	}
}
