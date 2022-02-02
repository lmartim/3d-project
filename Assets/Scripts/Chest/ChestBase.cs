using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class ChestBase : MonoBehaviour
{
    [Header("Animation")]
    public Animator animator;
    public string triggerOpen = "Open";

    [Header("Prompt")]
    public Canvas canvas;
    public TextMeshProUGUI uiTextValue;
    public KeyCode keyToOpen = KeyCode.E;
    public float tweenDuration = .2f;
    public Ease tweenEase = Ease.OutBack;

    [Space]
    public ChestItemCoin chestItem;

    private bool _playerIsNear = false;
    private bool _isOpened = false;
    private float _startScale;

    private void Awake()
    {
        uiTextValue.text = keyToOpen.ToString();
        _startScale = canvas.transform.localScale.x;
    }

    private void Update()
    {
        if (Input.GetKeyDown(keyToOpen) && _playerIsNear)
            OpenChest();
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerController p = other.GetComponent<PlayerController>();
        if (p != null && !_isOpened)
        {
            ShowPrompt();
            _playerIsNear = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        PlayerController p = other.GetComponent<PlayerController>();
        if (p != null && !_isOpened)
        {
            HidePrompt();
            _playerIsNear = false;
        }
    }

    [NaughtyAttributes.Button]
    private void OpenChest()
    {
        animator.SetTrigger(triggerOpen);
        _isOpened = true;
        HidePrompt();

        Invoke(nameof(ShowItem), .7f);
    }

    private void ShowItem()
    {
        chestItem.TriggerCollection();
    }

    private void ShowPrompt()
    {
        canvas.enabled = true;
        canvas.transform.localScale = Vector3.zero;
        canvas.transform.DOScale(_startScale, tweenDuration);
    }

    private void HidePrompt()
    {
        canvas.enabled = false;
        canvas.transform.DOScale(Vector3.zero, tweenDuration);
    }
}
