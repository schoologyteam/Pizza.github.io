using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DT_Scale : MonoBehaviour
{

    [SerializeField]
    private float size;

    [SerializeField]
    private float length;

    private Vector3 ogScale;
    private Vector3 ScaleTo;

    // Start is called before the first frame update
    void Start()
    {

        ogScale = transform.localScale;
        ScaleTo = ogScale * size;

        transform.DOScale(ScaleTo, length).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
    }

    
}
