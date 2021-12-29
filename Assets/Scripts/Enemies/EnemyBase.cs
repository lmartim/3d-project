using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Animation;

namespace Enemy
{
    public class EnemyBase : MonoBehaviour, IDamageable
    {
        public Collider enemyCollider;
        public FlashColor flashColor;

        public float startLife = 10f;
        private float _currentLife;

        public bool lookAtPlayer = false;

        private PlayerController _player;

        [Header("Animation")]
        [SerializeField] private AnimationBase _animationBase;

        [Header("Start Animation")]
        public float startAnimationDuration = .2f;
        public Ease startAnimationEase = Ease.OutBack;
        public bool startWithBornAnimation = true;

        [Header("Particles")]
        public ParticleSystem enemyParticleSystem;
        public Material enemyParticleMaterial;

        private void Awake()
        {
            Init();
        }

        private void Start()
        {
            _player = GameObject.FindObjectOfType<PlayerController>();
        }

        protected virtual void Init()
        {
            ResetLife();
            if (startWithBornAnimation) BornAnimation();
        }

        protected void ResetLife()
        {
            _currentLife = startLife;
        }

        protected virtual void Kill() 
        {
            OnKill();
        }


        protected virtual void OnKill()
        {
            if (enemyCollider != null) enemyCollider.enabled = false;

            if (enemyParticleSystem != null)
            {
                enemyParticleSystem.gameObject.GetComponent<Renderer>().material = enemyParticleMaterial;
                enemyParticleSystem.Emit(15);
            }

            Destroy(gameObject, 3f);
            PlayAnimationByTrigger(AnimationType.DEATH);
        }

        public void OnDamage(float f)
        {
            if (flashColor != null) flashColor.Flash();

            _currentLife -= f;

            if (_currentLife <= 0)
            {
                Kill();
            }
        }

        #region ANIMATIONS
        private void BornAnimation()
        {
            transform.DOScale(0, startAnimationDuration).SetEase(startAnimationEase).From();
        }    

        public void PlayAnimationByTrigger(AnimationType animationType)
        {
            _animationBase.PlayAnimationByTrigger(animationType);
        }
        #endregion

        public virtual void Update()
        {
            if (Input.GetKeyDown(KeyCode.T))
            {
                OnDamage(5f);
            }

            if (lookAtPlayer) transform.LookAt(_player.transform.position);
        }

        public void Damage(float damage)
        {
            OnDamage(damage);
        }

        public void Damage(float damage, Vector3 dir)
        {
            OnDamage(damage);
            transform.DOMove(transform.position - dir, .1f);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                PlayerController p = collision.transform.GetComponent<PlayerController>();

                if (p != null)
                {
                    p.healthBase.Damage(1);
                }
            }
        }
    }
}

