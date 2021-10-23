using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResolutionManager : MonoBehaviour
{
    public void Awake()
    {
        Screen.SetResolution(800, 600, false);
    }
}
