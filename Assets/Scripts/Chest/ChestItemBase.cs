using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestItemBase : MonoBehaviour
{
    public virtual void TriggerCollection() { }

    public virtual void Collect() { }

    public virtual void ShowItem() { }
}
