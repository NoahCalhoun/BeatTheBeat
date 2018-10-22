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

        public ControlType JudgeControl()
        {
            return ControlType.Left;
        }
    }

    private List<ClickEvent> ClickEventList = new List<ClickEvent>();

    private bool JudgingControl = false;
    private Vector2 ClickedPosition;
    private float ClickedTime;

    public float JudgeTime = 0.3f;
    public float JudgeMinSize = 50f;
    public float JudgeMaxSize = 300f;

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Click");
        return;

        JudgingControl = true;
        ClickedPosition = eventData.position;
        ClickedTime = Time.unscaledTime;
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

        Debug.Log("Down");
        return;

        if (JudgingControl == false)
            return;

        if (Time.unscaledTime - ClickedTime < JudgeTime)
            return;

        JudgingControl = false;
        Vector2 deltaPos = eventData.position - ClickedPosition;
        float size = deltaPos.magnitude;
        Debug.Log(string.Format("size : {0}", size));
        if (size < JudgeMinSize)
        {
            Debug.Log("Touched");
            //터치 판정
            return;
        }

        float angle = Mathf.Acos(deltaPos.x / size) * Mathf.Sign(deltaPos.y) * Mathf.Rad2Deg;
        if (angle <= 45f && angle >= -45f)
        {
            Debug.Log("Slide Right");
            //오른쪽 슬라이드
        }
        else if (angle > 45f && angle < 135f)
        {
            Debug.Log("Slide Up");
            //위쪽 슬라이드
        }
        else if (angle < -45f && angle > -135f)
        {
            Debug.Log("Slide Down");
            //아래쪽 슬라이드
        }
        else
        {
            Debug.Log("Slide Left");
            //왼쪽 슬라이드
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        var clickEvent = ClickEventList.Find((data) => { return data.ID == eventData.pointerId; });
        if (clickEvent == null)
            return;

        Debug.Log("Up");
        return;

        if (JudgingControl == false)
            return;

        JudgingControl = false;
        //터치 판정
        Debug.Log("size : 0");
        Debug.Log("Touched");
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("BeginDrag");
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("Drag");
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("EndDrag");
    }

    void Start ()
    {
		
	}
	
	void Update ()
    {
	}
}
