using UnityEngine;
using System.Collections;

public class Mover2D : MonoBehaviour
{
    public Transform Destination;

    public float LoopTime;
    public LeanTweenType LoopType;

    protected LTDescr tweening;

    protected virtual void Start()
    {
        tween();
    }

    protected virtual void tween()
    {
        tweening = LeanTween.move(this.gameObject, Destination.position, LoopTime).setLoopType(LoopType);
    }
}
