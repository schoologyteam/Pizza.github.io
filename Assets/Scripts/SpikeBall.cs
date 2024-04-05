using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeBall : MonoBehaviour
{

    [SerializeField]
    private float yMax; //The max where the objects moves in the y-axis.

    [SerializeField]
    private float time; //How long the movement back & forth takes.

    // Start is called before the first frame update
    void Start()
    {
        //DoTween up & down movement.
        transform.DOMoveY(transform.position.y + yMax, time).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);
    }


}
