using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DestructableItemBase : MonoBehaviour
{
    public HealthBase healthBase;

    [Header("Shake")]
    public float shakeDuration = .1f;
    public int shakeForce = 5;

    [Header("Coins")]
    public int coinsToDrop = 10;
    public GameObject coinPrefab;
    public Transform dropPosition;

    private void OnValidate()
    {
        if (healthBase == null) healthBase = GetComponent<HealthBase>();
    }

    private void Awake()
    {
        OnValidate();
        healthBase.OnDamage += OnDamage;
    }

    private void OnDamage(HealthBase h)
    {
        transform.DOShakeScale(shakeDuration, Vector3.up, shakeForce);
        DropGroupOfCoins();
    }

    private void DropCoins()
    {
        var i = Instantiate(coinPrefab);
        i.transform.position = dropPosition.position;
        i.transform.DOScale(0, 1f).SetEase(Ease.OutBack).From();
    }

    private void DropGroupOfCoins()
    {
        StartCoroutine(DropGroupOfCoinsCoroutine());
    }

    IEnumerator DropGroupOfCoinsCoroutine()
    {
        for (int i = 0; i < coinsToDrop; i++)
        {
            DropCoins();
            yield return new WaitForSeconds(.1f);
        }
    }
}
