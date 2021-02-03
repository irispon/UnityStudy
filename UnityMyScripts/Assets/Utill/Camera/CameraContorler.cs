using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utill
{
    public class CameraContorler :MonoBehaviour
    {
        public Vector3 fiexRoation;
        public Vector3 fiexPosition;
        public float cameraSpeed;
        Camera mainCamera;

        IEnumerator focusing;
        private void Awake()
        {
            mainCamera = GetComponent<Camera>();
        }
        public void SetFocus(GameObject focuse,Action callBack=null,bool rotateFollow=false)
        {
           
          

            if (focusing != null)
            {
                StopCoroutine(focusing);
            }
            focusing = Focusing(focuse, rotateFollow, callBack:callBack);
            StartCoroutine(focusing);

        }

        public void CamToCam(Camera camera,bool shadow = false)
        {

            if (focusing != null)
            {
                StopCoroutine(focusing);
            }
            focusing = CameraToCamera(camera, shadow);
            StartCoroutine(focusing);

        }
        private IEnumerator Focusing(GameObject focuse, bool rotateFollow=false,Action callBack = null)
        {
            
            Vector3 focusePosition;
            Vector3 focuseRotation;
            Vector3 direction;
            while (true)
            {
                direction = (transform.position - focuse.transform.position).normalized;

                focusePosition = focuse.transform.position + fiexPosition;
                focuseRotation = Vector3.Cross(Vector3.up,direction) + fiexRoation+focuse.transform.eulerAngles;


                if ((focuse.transform.position != mainCamera.transform.position) || (focuse.transform.rotation.eulerAngles == transform.rotation.eulerAngles))
                {
                    transform.position = Vector3.Lerp(transform.position, focusePosition, Time.deltaTime * 2f);

                    if(rotateFollow)
                    transform.rotation = Quaternion.Slerp(transform.rotation,Quaternion.Euler(focuseRotation), Time.deltaTime * 2f);

                    yield return new WaitForFixedUpdate();
                }
                else
                {
                    if (callBack != null)
                        callBack();
                    yield break;
                }

            }


        }
        public void Attach(GameObject focuse, Action callBack = null, bool rotateFollow = false)
        {
            transform.SetParent(focuse.transform);
            if (focusing != null)
            {
                StopCoroutine(focusing);
            }
            focusing = AttachFocus(focuse, rotateFollow, callBack: callBack);
            StartCoroutine(focusing);
        }

        private IEnumerator AttachFocus(GameObject focuse, bool rotateFollow = false, Action callBack = null)
        {

            Vector3 focusePosition;
            Vector3 focuseRotation;

            while (true)
            {
          

                focusePosition = new Vector3(0,0,0) + fiexPosition;
                focuseRotation =new Vector3(0,0,0)+fiexRoation;


                if ((focuse.transform.position != mainCamera.transform.position) || (focuse.transform.rotation.eulerAngles == transform.rotation.eulerAngles))
                {
                    transform.localPosition = Vector3.Lerp(transform.localPosition, focusePosition, Time.deltaTime * 2f);

                    if (rotateFollow)
                        transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.Euler(focuseRotation), Time.deltaTime * 2f);

                    yield return new WaitForFixedUpdate();
                }
                else
                {
                    if (callBack != null)
                        callBack();
                    yield break;
                }

            }


        }

        private IEnumerator CameraToCamera(Camera camera,bool shadow=false)
        {
            if (shadow == false)
            {
            Vector3 mainCameraPosition = mainCamera.transform.position;
            Vector3 mainCameraRotation = mainCamera.transform.rotation.eulerAngles;

            mainCamera.transform.position = camera.transform.position;
            mainCamera.transform.rotation = camera.transform.rotation;

         
                mainCamera.enabled = true;
                camera.enabled = false;
       


            Vector3 lerpPosition;
            Quaternion lerpRotation;
        
            while (true)
            {



                if ((mainCameraPosition != mainCamera.transform.position) || (mainCameraRotation == transform.rotation.eulerAngles))
                {
                    lerpPosition = Vector3.Lerp(transform.position, mainCameraPosition, Time.deltaTime * 2f);
                    lerpRotation= Quaternion.Slerp(transform.rotation, Quaternion.Euler(mainCameraRotation), Time.deltaTime * 2f);
                    transform.position = lerpPosition;
                    transform.rotation = lerpRotation;
                    camera.transform.position = lerpPosition;
                    camera.transform.rotation = lerpRotation;
                    yield return new WaitForFixedUpdate();
                }
                else
                {

                    yield break;
                }

            }
            }
            else
            {
                while (true)
                {
                    mainCamera.transform.position = camera.transform.position;
                    mainCamera.transform.rotation = camera.transform.rotation;
                    yield return new WaitForSeconds(0.2f);
                }


         

            }

        }


    }
}

