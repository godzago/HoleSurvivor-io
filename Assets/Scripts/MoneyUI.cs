using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MoneyUI : MonoBehaviour
{
    [SerializeField] Transform scoreAreaTransform;
    Sequence goldAnimation;

    void Awake()
    {
        SetAnim();
    }

    void SetAnim()
    {
        scoreAreaTransform = GameObject.FindGameObjectWithTag("MoneUI").transform;
        goldAnimation = DOTween.Sequence();

        goldAnimation.Append(transform.DOMove(scoreAreaTransform.position, 2).SetEase(Ease.InOutBack)).OnComplete(() => Destroy(gameObject));
    }
}
