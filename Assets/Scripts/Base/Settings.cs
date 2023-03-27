using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour
{
    private enum Tutorial { HorizontalSwerve = 0, JoystickSwerve = 1, Drag = 2 }
    [SerializeField] private Tutorial tutorial;

    [Range(60, 120)]
    [SerializeField] private int TFrame = 60;

    private void Start()
    {
        Application.targetFrameRate = TFrame;
    }

    public int GetTutorialIndex()
    {
        return (int)tutorial;
    }
}
