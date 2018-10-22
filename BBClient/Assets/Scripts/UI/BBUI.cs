using UnityEngine;
using UnityEngine.UI;

public class BBUI : BBBase
{
    private RectTransform mRt;
    public RectTransform Rt { get { if (mRt == null) mRt = transform as RectTransform; return mRt; } }


    private Image mImg;
    public Image Img { get { if (mImg == null) mImg = GetComponent<Image>(); return mImg; } }
}
