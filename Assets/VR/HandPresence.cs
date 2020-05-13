using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class HandPresence : MonoBehaviour
{
    public InputDeviceCharacteristics controllerCharacteristics;
    public Animator[] claws;
    private InputDevice targetDevice;
    private Animator handAnimator;

    void Start()
    {
        List<InputDevice> devices = new List<InputDevice>();
        InputDevices.GetDevicesWithCharacteristics(controllerCharacteristics, devices);
        handAnimator = gameObject.GetComponentInChildren<Animator>();
        if(devices.Count > 0)
        {
            targetDevice = devices[0];
        }
    }

    // Update is called once per frame
    void Update()
    {
            UpdateHandAnimation();
  
    }

    void UpdateHandAnimation()
    {
        if(targetDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue))
        {
            if (triggerValue < 0.8f && triggerValue > 0f)
            {
                //RequestHaptic(0, triggerValue, 0.05f);
            }
            handAnimator.SetFloat("Trigger", triggerValue);
            foreach (Animator claw in claws)
            {
                claw.SetFloat("Trigger", triggerValue);
            }
        }
        else
        {
            foreach (Animator claw in claws)
            {
                claw.SetFloat("Trigger", 0);
            }
            handAnimator.SetFloat("Trigger", 0);
        }

        if (targetDevice.TryGetFeatureValue(CommonUsages.grip, out float gripValue))
        {
            
            
            handAnimator.SetFloat("Grip", gripValue);
        }
        else
        {
            handAnimator.SetFloat("Grip", 0);
        }
    }

    public void RequestHaptic(uint channel, float amplitude, float duration)
    {
        //Debug.Log("recieve");
        HapticCapabilities capabilities;
        if (targetDevice.TryGetHapticCapabilities(out capabilities))
        {
            if (capabilities.supportsImpulse)
            {
                targetDevice.SendHapticImpulse(channel, amplitude, duration);
            }
        }
        else Debug.Log("Haptic failed");
    }

    public void jx(string from)
    {
        //Debug.Log(from);
        RequestHaptic(0, 0.9f, 0.3f);
    }
}
