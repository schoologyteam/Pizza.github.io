using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DT_Scale : MonoBehaviour
{

    [SerializeField]
    private float size; //Size to scale to

    [SerializeField]
    private float length; //How long the Yoyo effect takes

    private Vector3 ogScale; //Original scale of the object
    private Vector3 ScaleTo; //End value where the object scales to

    // Start is called before the first frame update
    void Start()
    {

        ogScale = transform.localScale;
        ScaleTo = ogScale * size;

        //DoTween Scaling starts
        transform.DOScale(ScaleTo, length).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
    }

    
}
