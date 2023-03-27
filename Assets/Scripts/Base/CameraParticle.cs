using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraParticle : MonoBehaviour
{
    [SerializeField] private GameObject WinParticle;

    public void PlayWinParticle()
    {
        WinParticle.SetActive(true);
    }
}
