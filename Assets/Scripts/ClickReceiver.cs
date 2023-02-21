using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

class ClickReceiver : MonoBehaviour
{
    private Collider2D collider2d;
    private Button button;

    void Start()
    {
        collider2d = GetComponent<Collider2D>();
        button = GetComponent<Button>();
    }

    void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            var touchPosition = new Vector2(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y);

            if (collider2d == Physics2D.OverlapPoint(touchPosition))
            {
                ExecuteEvents.Execute(button.gameObject, new BaseEventData(EventSystem.current), ExecuteEvents.submitHandler);
            }
        }
    }
}