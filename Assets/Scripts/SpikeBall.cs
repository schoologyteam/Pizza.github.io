using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeBall : MonoBehaviour
{

    [SerializeField]
    private float yMax;

    [SerializeField]
    private float time;

    // Start is called before the first frame update
    void Start()
    {
        transform.DOMoveY(transform.position.y + yMax, time).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);
    }


}
