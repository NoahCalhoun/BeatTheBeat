using System.Collections.Generic;

using UnityEngine;
using UnityEngine.EventSystems;


public class BBInputRect : BBUI, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    class ClickEvent
    {
        public int ID;
        public Vector2 ClickedPosition;
        public Vector2 LastPosition;
        public float ClickedTime;

        public float GetDragLengthSqr
        {
            get
            {
                return (LastPosition - ClickedPosition).sqrMagnitude;
            }
        }

        public ControlType JudgeControl(float minSize)
        {
            Vector2 deltaPos = LastPosition - ClickedPosition;
            float sizeSqr = deltaPos.sqrMagnitude;
            if (sizeSqr < minSize * minSize)
            {
                Debug.Log("Touched");
                return ClickedPosition.x < MiddleX ? ControlType.Left : ControlType.Right;
            }
            
            float size = Mathf.Sqrt(sizeSqr);
            float angle = Mathf.Acos(deltaPos.x / size) * Mathf.Sign(deltaPos.y) * Mathf.Rad2Deg;
            if (angle <= 45f && angle >= -45f)
            {
                Debug.Log("Slide Right");
                return ControlType.SlideRight;
            }
            else if (angle > 45f && angle < 135f)
            {
                Debug.Log("Slide Up");
                return ControlType.SlideUp;
            }
            else if (angle < -45f && angle > -135f)
            {
                Debug.Log("Slide Down");
                return ControlType.SlideDown;
            }
            else
            {
                Debug.Log("Slide Left");
                return ControlType.SlideLeft;
            }
        }
    }

    private List<ClickEvent> ClickEventList = new List<ClickEvent>();

    public float JudgeTime = 0.15f;
    public float JudgeMinSize = 20f;
    public float JudgeMaxSize = 100f;

    static public float MiddleX { get; private set; }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (ClickEventList.Find((data) => { return data.ID == eventData.pointerId; }) != null)
            return;

        ClickEventList.Add(new ClickEvent
        {
            ID = eventData.pointerId,
            ClickedPosition = eventData.position,
            LastPosition = eventData.position,
            ClickedTime = Time.unscaledTime
        });
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        var clickEvent = ClickEventList.Find((data) => { return data.ID == eventData.pointerId; });
        if (clickEvent == null)
            return;

        BBGameMgr.OnControl(clickEvent.JudgeControl(JudgeMinSize));
        ClickEventList.Remove(clickEvent);
    }

    public void OnDrag(PointerEventData eventData)
    {
        var clickEvent = ClickEventList.Find((data) => { return data.ID == eventData.pointerId; });
        if (clickEvent == null)
            return;
        
        clickEvent.LastPosition = eventData.position;
    }

    void Start()
    {
        MiddleX = Rt.rect.width * 0.5f;
    }
	
	void Update ()
    {
        for (int i = 0; i < ClickEventList.Count;)
        {
            var clickEvent = ClickEventList[i];

            if (Time.unscaledTime - clickEvent.ClickedTime >= JudgeTime
                 || clickEvent.GetDragLengthSqr > JudgeMaxSize * JudgeMaxSize)
            {
                BBGameMgr.OnControl(clickEvent.JudgeControl(JudgeMinSize));
                ClickEventList.RemoveAt(i);
            }
            else
            {
                ++i;
            }
        }
	}
}
