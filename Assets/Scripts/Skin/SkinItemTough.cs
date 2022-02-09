using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Skin
{
    public class SkinItemTough : SkinItemBase
    {
        public float newDefence = 2f;

        public override void Collect()
        {
            base.Collect();
            PlayerController.Instance.healthBase.ChangeDamageMultiplier(newDefence, duration);
        }
    }
}
