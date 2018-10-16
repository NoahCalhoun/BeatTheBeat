using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.EventSystems;

public class TestUI : MonoBehaviour, IPointerClickHandler
{
    public delegate void OnClick();

    public OnClick OnClickEvent;

    public void OnPointerClick(PointerEventData eventData)
    {
        OnClickEvent();
    }

    // Use this for initialization
    void Start ()
    {
        OnClickEvent = () => { GetComponent<RectTransform>().localPosition += new Vector3(5f, 0f); };
    }
	
	// Update is called once per frame
	void Update ()
    {
        transform.localPosition += (new Vector3(5f, 0f) * Time.deltaTime);
    }
}
