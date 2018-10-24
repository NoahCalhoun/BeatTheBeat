using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class BBInputRect : BBUI, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler
    , IBeginDragHandler, IDragHandler, IEndDragHandler
{
    class ClickEvent
    {
        public int ID;
        public Vector2 ClickedPosition;
        public Vector2 LastPosition;
        public float ClickedTime;
        public bool Dragged;

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
            float size = deltaPos.magnitude;
            Debug.Log(string.Format("size : {0}", size));
            if (size < minSize)
            {
                Debug.Log("Touched");
                //터치 판정
                return ControlType.Left;
            }

            float angle = Mathf.Acos(deltaPos.x / size) * Mathf.Sign(deltaPos.y) * Mathf.Rad2Deg;
            if (angle <= 45f && angle >= -45f)
            {
                Debug.Log("Slide Right");
                return ControlType.SlideRight;
                //오른쪽 슬라이드
            }
            else if (angle > 45f && angle < 135f)
            {
                Debug.Log("Slide Up");
                return ControlType.SlideUp;
                //위쪽 슬라이드
            }
            else if (angle < -45f && angle > -135f)
            {
                Debug.Log("Slide Down");
                return ControlType.SlideDown;
                //아래쪽 슬라이드
            }
            else
            {
                Debug.Log("Slide Left");
                return ControlType.SlideLeft;
                //왼쪽 슬라이드
            }
        }
    }

    private List<ClickEvent> ClickEventList = new List<ClickEvent>();

    public float JudgeTime = 0.15f;
    public float JudgeMinSize = 20f;
    public float JudgeMaxSize = 100f;

    public void OnPointerClick(PointerEventData eventData)
    {
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (ClickEventList.Find((data) => { return data.ID == eventData.pointerId; }) != null)
            return;

        ClickEventList.Add(new ClickEvent
        {
            ID = eventData.pointerId,
            ClickedPosition = eventData.position,
            LastPosition = eventData.position,
            ClickedTime = Time.unscaledTime,
            Dragged = false
        });
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        var clickEvent = ClickEventList.Find((data) => { return data.ID == eventData.pointerId; });
        if (clickEvent == null)
            return;

        clickEvent.JudgeControl(JudgeMinSize);
        ClickEventList.Remove(clickEvent);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
    }

    public void OnDrag(PointerEventData eventData)
    {
        var clickEvent = ClickEventList.Find((data) => { return data.ID == eventData.pointerId; });
        if (clickEvent == null)
            return;

        clickEvent.LastPosition = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
    }

    void Start ()
    {
		
	}
	
	void Update ()
    {
        for (int i = 0; i < ClickEventList.Count;)
        {
            if (Time.unscaledTime - ClickEventList[i].ClickedTime >= JudgeTime
                 || ClickEventList[i].GetDragLengthSqr > JudgeMaxSize * JudgeMaxSize)
            {
                ClickEventList[i].JudgeControl(JudgeMinSize);
                ClickEventList.RemoveAt(i);
            }
            else
            {
                ++i;
            }
        }
	}
}
