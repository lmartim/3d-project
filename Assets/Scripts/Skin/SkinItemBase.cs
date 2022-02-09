using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Skin
{
    public class SkinItemBase : MonoBehaviour
    {
        public SkinType skinType;
        public float duration = 2f;

        public string compareTag = "Player";

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(compareTag))
            {
                Collect();
            }
        }

        public virtual void Collect()
        {
            var setup = SkinManager.Instance.GetSetupByType(skinType);

            PlayerController.Instance.ChangeTexture(setup, duration);

            HideObject();
        }

        private void HideObject()
        {
            gameObject.SetActive(false);
        }
    }
}

