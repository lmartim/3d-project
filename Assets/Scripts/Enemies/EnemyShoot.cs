using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class EnemyShoot : EnemyBase
    {
        [Header("Gun")]
        public GunBase gunBase;

        protected override void Init()
        {
            base.Init();

            gunBase.StartShoot();
        }
    }
}
