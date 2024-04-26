using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Archer
{


    public class Bow : MonoBehaviour
    {

        // Referencia a la acción de Input para disparar
        [SerializeField]
        private InputActionReference fireInputReference;

        // Una referencia al prefab de la flecha
        [SerializeField]
        private GameObject arrowPrefab;

        // Cantidad de fuerza que aplicaremos al disparar la flecha
        [SerializeField]
        private float force;
        
        // Una referencia a un transform que servirá de punto de referencia para disparar la flecha
        [SerializeField]
        private Transform handPosition;

        [SerializeField]
        private float verticalOffset;

      

        private Animator animator;
        private AudioSource audioSource;

        private void Awake()
        {
           
            // Nos subscribimos al evento de input de disparo (el espacio o el botón A).
            fireInputReference.action.performed += Action_performed;
            animator = GetComponent<Animator>();
            audioSource = GetComponent<AudioSource>();
        }

        private void Action_performed(InputAction.CallbackContext obj)
        {
            // Cuando se pulsa espacio, producimos un disparo
            StartCoroutine(Shoot());
        }

        private IEnumerator Shoot()
        {

            animator.SetTrigger("Shoot");

            yield return new WaitForSeconds(0.3f);


            GameObject arrow = Instantiate(arrowPrefab, handPosition.position, handPosition.rotation);
            //arrow.transform.position = handPosition.position;
            Vector3 launchVector = transform.forward;
            launchVector.y += verticalOffset;
            launchVector *= force;
            audioSource.Play();

            //Debug.Log(launchVector);
            arrow.GetComponent<Rigidbody>().AddForce(launchVector);
        }
    }

}