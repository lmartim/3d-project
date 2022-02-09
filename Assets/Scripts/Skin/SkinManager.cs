using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Singleton;

namespace Skin
{
    public enum SkinType
    {
        BASE,
        SPEED,
        TOUGH
    }

    public class SkinManager : Singleton<SkinManager>
    {
        public List<SkinSetup> skinSetups;

        public SkinSetup GetSetupByType(SkinType skinType)
        {
            return skinSetups.Find(i => i.skinType == skinType);
        }
    }

    [System.Serializable]
    public class SkinSetup
    {
        public SkinType skinType;
        public Texture2D texture;
    }
}
