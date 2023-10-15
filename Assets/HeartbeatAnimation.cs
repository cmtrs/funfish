using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HeartbeatAnimation : MonoBehaviour
{
    Vector3 originalScale;
    [SerializeField]
    float scaleUpPercentage;
    [SerializeField]
    float rotationSpeed;
    [SerializeField]
    float scaleSpeed;

    void Start()
    {
        originalScale = this.transform.localScale;
        transform.DOLocalRotate(new Vector3(0, 0, 360), rotationSpeed, RotateMode.FastBeyond360).SetEase(Ease.Linear);/*.SetLoops(-1, LoopType.Incremental).SetRelative();*/

        var sequence = DOTween.Sequence()
            .Append(this.transform.DOScale(new Vector3(originalScale.x * scaleUpPercentage, originalScale.y * scaleUpPercentage, originalScale.z * scaleUpPercentage), scaleSpeed)).SetEase(Ease.Linear)
            .Append(this.transform.DOScale(originalScale, scaleSpeed)).SetEase(Ease.Linear);


        sequence.SetLoops(-1, LoopType.Restart);
    }
}
