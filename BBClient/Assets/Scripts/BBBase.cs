using UnityEngine;
using UnityEngine.UI;

public class BBBase : MonoBehaviour
{
    private Transform mTm;
    public Transform Tm { get { if (mTm == null) mTm = transform; return mTm; } }


    private GameObject mGo;
    public GameObject Go { get { if (mGo == null) mGo = gameObject; return mGo; } }
}