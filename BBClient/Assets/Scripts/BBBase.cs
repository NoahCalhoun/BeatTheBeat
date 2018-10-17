using UnityEngine;
using UnityEngine.UI;

public class BBBase : MonoBehaviour
{
    private Transform mTm;
    public Transform Tm { get { if (mTm == null) mTm = transform; return mTm; } }


    private RectTransform mRt;
    public RectTransform Rt { get { if (mRt == null) mRt = transform as RectTransform; return mRt; } }


    private GameObject mGo;
    public GameObject Go { get { if (mGo == null) mGo = gameObject; return mGo; } }


    private Image mImg;
    public Image Img { get { if (mImg == null) mImg = GetComponent<Image>(); return mImg; } }
}