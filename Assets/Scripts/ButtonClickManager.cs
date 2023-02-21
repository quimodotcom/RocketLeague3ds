using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonClickManager : MonoBehaviour
{
    private Button[] buttons;

    void Start()
    {
        buttons = FindObjectsOfType<Button>();
        foreach (var b in buttons)
        {
            b.gameObject.AddComponent<BoxCollider2D>().size = b.GetComponent<RectTransform>().rect.size;
            b.gameObject.AddComponent<ClickReceiver>();
        }
    }
}