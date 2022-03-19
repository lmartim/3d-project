using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Items
{
    public class ItemCollectableBase : MonoBehaviour
    {
        public SFXType sfxType;
        public ItemType itemType;

        public string compareTag = "Player";
        public Collider thisCollider;
        public ParticleSystem thisParticleSystem;
        public float timeToHide = 3f;
        public GameObject graphicItem;

        [Header("Sounds")]
        public AudioSource audioSource;

        private void OnTriggerEnter(Collider collision)
        {
            if (collision.transform.CompareTag(compareTag))
            {
                Collect();
            }
        }

        private void PlaySFX()
        {
            SFXPool.Instance.Play(sfxType);
        }

        protected virtual void Collect()
        {
            PlaySFX();
            if (graphicItem != null && graphicItem.activeSelf)
            {
                graphicItem.SetActive(false);
                OnCollect();
            }
            Invoke("HideObject", timeToHide);
        }

        private void HideObject()
        {
            gameObject.SetActive(false);
        }

        protected virtual void OnCollect()
        {
            if (thisCollider != null) thisCollider.enabled = false;
            if (thisParticleSystem != null) thisParticleSystem.Play();
            if (audioSource != null) audioSource.Play();

            ItemManager.Instance.AddByType(itemType);
        }
    }
}
