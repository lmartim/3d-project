using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Items;

public class ChestItemCoin : ChestItemBase
{
    public int coinsNumber = 5;
    public GameObject coinObject;

    private List<GameObject> _items = new List<GameObject>();

    public override void TriggerCollection()
    {
        base.TriggerCollection();

        for (int i = 0; i < coinsNumber; i++)
        {
            var tweenDelay = (((float)i) * 100f) / 250f;

            var item = Instantiate(coinObject);
            item.transform.position = transform.position;
            _items.Add(item);

            item.transform.DOScale(0, .2f).SetDelay(tweenDelay).SetEase(Ease.OutBack).From();
            
            item.transform.DOMoveY(3f, .2f).SetDelay(tweenDelay).SetRelative();
            item.transform.DOScale(0, .2f).SetDelay(tweenDelay + .07f);

            Invoke(nameof(AddCoin), tweenDelay);
        }
    }

    public void AddCoin()
    {
        ItemManager.Instance.AddByType(ItemType.COIN);
    }

    public override void Collect()
    {
        base.Collect();

        foreach(var i in _items)
        {
            i.transform.DOMoveY(2f, .2f).SetRelative();
            i.transform.DOScale(0, .2f).SetDelay(.2f);
            ItemManager.Instance.AddByType(ItemType.COIN);
        }
    }

    public override void ShowItem()
    {
        base.ShowItem();
        CreateItems();
    }

    private void CreateItems()
    {
        for(int i = 0; i < coinsNumber; i++)
        {
            var item = Instantiate(coinObject);
            item.transform.position = transform.position;
            item.transform.DOScale(0, .2f).SetEase(Ease.OutBack).From();
            _items.Add(item);
        }
    }
}
