using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;

public class HoleManager : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<Rigidbody>().useGravity = true;
        }

        if (other.gameObject.CompareTag("Enemy_AI"))
        {
            other.gameObject.GetComponent<NavMeshAgent>().baseOffset = -3;
            other.gameObject.SetActive(false);
        }
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.transform.DOMove(new Vector3(transform.position.x, transform.position.y - 5, transform.position.z), 2);
        }
    }
}
