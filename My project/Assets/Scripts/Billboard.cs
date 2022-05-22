using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
   public Transform cam;

   public void Start()
   {
      cam = CameraManager.instance.camera.transform;
   }

   void LateUpdate()
   {
      transform.LookAt(transform.position + cam.forward);
   }
}
