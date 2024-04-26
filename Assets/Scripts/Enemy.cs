using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Archer
{

    public class Enemy : MonoBehaviour, IScoreProvider
    {

        // Cuántas vidas tiene el enemigo
        [SerializeField]
        private int hitPoints;

        [SerializeField]
        private GameObject ligth;

        private Animator animator;
        private AudioSource audioSource;

        public event IScoreProvider.ScoreAddedHandler OnScoreAdded;

        private void Awake()
        {
            animator = GetComponent<Animator>();
            audioSource = GetComponent<AudioSource>();
        }

        // Método que se llamará cuando el enemigo reciba un impacto
        public void Hit()
        {
            audioSource.Play();
            animator.SetTrigger("Hit");
            Debug.Log("L¡Hit");
            hitPoints--;
            if (hitPoints <= 0 )
            {
                Die();
            }
        }

        private void Die()
        {
            StartCoroutine(DieCoroutine());
        }

        IEnumerator DieCoroutine()
        {
            animator.SetTrigger("Die");
            ligth.SetActive(true);
            yield return new WaitForSeconds(3);
            ligth.SetActive(false);
            Destroy(gameObject);
        }
    }

}