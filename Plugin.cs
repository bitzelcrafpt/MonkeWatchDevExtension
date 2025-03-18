using BepInEx;
using System;
using UnityEngine;
using BananaWatch;
using UnityEngine.InputSystem;
using Cinemachine;
namespace MonkeWatchKeyboard
{
    [BepInPlugin(PluginInfo.GUID, PluginInfo.Name, PluginInfo.Version)]
    public class Plugin : BaseUnityPlugin
    {
        static bool shouldMenuBeOpen = false;
        static bool debounceDone = true;
        void HandleOpeningMenu()
        {
            if (Keyboard.current.tKey.isPressed)
            {
                GameObject.Find("Player Objects/Local VRRig/Local Gorilla Player/RigAnchor/rig/body/shoulder.L/upper_arm.L/forearm.L/hand.L/BananaWatch(Clone)/Plane").SetActive(true);
            }
            if (Keyboard.current.yKey.wasPressedThisFrame && shouldMenuBeOpen == false && debounceDone)
            {
                shouldMenuBeOpen = true;
                debounceDone = false;
            }
            if (Keyboard.current.yKey.wasPressedThisFrame && shouldMenuBeOpen == true && debounceDone)
            {
                shouldMenuBeOpen = false;
                debounceDone = false;
            }
            if (Keyboard.current.yKey.wasReleasedThisFrame && debounceDone == false)
            {
                debounceDone = true;
            }
            if (shouldMenuBeOpen)
            {
                GameObject.Find("Player Objects/Local VRRig/Local Gorilla Player/RigAnchor/rig/body/shoulder.L/upper_arm.L/forearm.L/hand.L/BananaWatch(Clone)/Plane").SetActive(true);
            }
        }
        void HandleArrowKeys()
        {
            if (Keyboard.current.jKey.wasPressedThisFrame)
            {
                BananaWatch.BananaWatch.Instance._CurrentPage.ButtonPressed(BananaWatchButton.Left);
            }
            if (Keyboard.current.iKey.wasPressedThisFrame)
            {
                BananaWatch.BananaWatch.Instance._CurrentPage.ButtonPressed(BananaWatchButton.Up);
            }
            if (Keyboard.current.kKey.wasPressedThisFrame)
            {
                BananaWatch.BananaWatch.Instance._CurrentPage.ButtonPressed(BananaWatchButton.Down);
            }
            if (Keyboard.current.lKey.wasPressedThisFrame)
            {
                BananaWatch.BananaWatch.Instance._CurrentPage.ButtonPressed(BananaWatchButton.Right);
            }
            if(Keyboard.current.nKey.wasPressedThisFrame)
            {
                BananaWatch.BananaWatch.Instance._CurrentPage.ButtonPressed(BananaWatchButton.Enter);
            }
            if (Keyboard.current.mKey.wasPressedThisFrame)
            {
                BananaWatch.BananaWatch.Instance._CurrentPage.ButtonPressed(BananaWatchButton.Back);
            }
        }
        private Camera bananaWatchCam;
        private GameObject bananaWatchCamObj;
        private bool camDebounceDone = true;
        private bool shouldCamBeEnabled = false;
        void HandleCamera()
        {
            if(bananaWatchCamObj != null)
            {
                if (Keyboard.current.cKey.wasPressedThisFrame && shouldCamBeEnabled == false && camDebounceDone)
                {
                    shouldCamBeEnabled = true;
                    camDebounceDone = false;
                }
                if (Keyboard.current.cKey.wasPressedThisFrame && shouldCamBeEnabled == true && camDebounceDone)
                {
                    shouldCamBeEnabled = false;
                    camDebounceDone = false;
                }
                if (Keyboard.current.cKey.wasReleasedThisFrame && camDebounceDone == false)
                {
                    camDebounceDone = true;
                }
                if (shouldCamBeEnabled)
                {
                    bananaWatchCam.enabled = true;
                }
                else
                {
                    bananaWatchCam.enabled = false;
                }
            }
            else
            {
                bananaWatchCamObj = new GameObject("BananaWatchCam");
                Cinemachine3rdPersonFollow cinemachine3rdPersonFollow = FindObjectOfType<Cinemachine3rdPersonFollow>();
                Camera componentInParent = cinemachine3rdPersonFollow.gameObject.GetComponentInParent<Camera>();
                bananaWatchCam = bananaWatchCamObj.AddComponent<Camera>();
                bananaWatchCam.fieldOfView = 90f;

                bananaWatchCam.nearClipPlane = componentInParent.nearClipPlane;
                bananaWatchCam.farClipPlane = componentInParent.farClipPlane;
                bananaWatchCam.targetDisplay = componentInParent.targetDisplay;
                bananaWatchCam.cullingMask = componentInParent.cullingMask;
                bananaWatchCam.depth = componentInParent.depth + 1f;
                bananaWatchCam.targetDisplay = componentInParent.targetDisplay;
                bananaWatchCam.enabled = false;
                //orig local position -0.0709 0.2691 0.1999
                //orig rotation 12.9136 111.2 34
                bananaWatchCamObj.transform.parent = GameObject.Find("Player Objects/Local VRRig/Local Gorilla Player/RigAnchor/rig/body/shoulder.L/upper_arm.L/forearm.L/hand.L/").transform;
                bananaWatchCamObj.transform.position = new Vector3(-68.5581f, 11.9536f, -83.8355f);
                bananaWatchCamObj.transform.localPosition = new Vector3(-0.0709f, 0.2692f, 0.1999f);
                bananaWatchCamObj.transform.localRotation = Quaternion.Euler(12.9136f, 111.2f, 34f);
                bananaWatchCamObj.transform.localScale = Vector3.one;
            }
        }
        void LateUpdate()
        {
            if(BananaWatch.BananaWatch.Instance.isInitialized)
            {
                HandleOpeningMenu();
                HandleArrowKeys();
                HandleCamera();
            }
        }
    }
}
