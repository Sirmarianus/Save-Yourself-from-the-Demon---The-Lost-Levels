using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    #region Singleton

    public static CameraManager instance;

    void Awake()
    {
        instance = this;
    }

    #endregion

    public GameObject camera;
}
