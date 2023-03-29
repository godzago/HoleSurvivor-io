using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;

public class HoleManager : MonoBehaviour
{
    public bool GameOver;

    private void Awake()
    {
        GameOver = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<Rigidbody>().useGravity = true;
        }

        if (other.gameObject.CompareTag("EnemyAI"))
        {
            other.gameObject.GetComponent<NavMeshAgent>().transform.DOMove(new Vector3(transform.position.x, transform.position.y - 5, transform.position.z), 2);
        }
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.transform.DOMove(new Vector3(transform.position.x, transform.position.y - 5, transform.position.z), 2);
            GameOver = true;
        }
    }
}
