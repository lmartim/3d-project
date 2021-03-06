using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBase : MonoBehaviour, IDamageable
{
    public FlashColor flashColor;

    public bool destroyOnKill = false;
    public float startLife = 10f;
    public float _currentLife;

    public Action<HealthBase> OnDamage;
    public Action<HealthBase> OnKill;

    public List<UIFillUpdater> uiFillUpdater;

    public float damageMultiplier = 1f;

    private void Awake()
    {
        Init();
    }

    public void Init()
    {
        ResetLife();
    }

    public void ResetLife()
    {
        _currentLife = startLife;
        UpdateUI();
    }

    protected virtual void Kill()
    {
        if (destroyOnKill)
            Destroy(gameObject, 3f);

        OnKill?.Invoke(this);
    }

    public void Damage(float f)
    {
        _currentLife -= f * damageMultiplier;
        if (flashColor != null) flashColor.Flash();

        if (_currentLife <= 0)
        {
            Kill();
        }

        UpdateUI();
        OnDamage?.Invoke(this);
    }

    public void Damage(float damage, Vector3 dir)
    {
        Damage(damage);
    }

    private void UpdateUI()
    {
        if (uiFillUpdater != null)
        {
            uiFillUpdater.ForEach(i => i.UpdateValue((float)_currentLife / startLife));
        }
    }

    public void ChangeDamageMultiplier(float newDamageMultiplier, float duration)
    {
        StartCoroutine(ChangeDamageMultiplierCoroutine(newDamageMultiplier, duration));
    }

    IEnumerator ChangeDamageMultiplierCoroutine(float newDamageMultiplier, float duration)
    {
        damageMultiplier = newDamageMultiplier;

        yield return new WaitForSeconds(duration);

        damageMultiplier = 1f;
    }
}
