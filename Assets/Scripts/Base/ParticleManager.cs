using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    [SerializeField] private bool OnDestroy = false;
    [SerializeField] private float ActiveTime = 0;

    private void OnEnable()
    {
        Invoke(nameof(DeActive), ActiveTime);
    }

    void DeActive()
    {
        if (OnDestroy)
        {
            Destroy(gameObject);
            return;
        }

        gameObject.SetActive(false);
    }
}
