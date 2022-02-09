using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Skin
{
    public class SkinItemSpeed : SkinItemBase
    {
        public float newSpeed = 2f;

        public override void Collect()
        {
            base.Collect();
            PlayerController.Instance.ChangeSpeed(newSpeed, duration);
        }
    }
}
