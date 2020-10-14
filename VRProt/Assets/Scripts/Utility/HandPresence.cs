using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

namespace Utility
{
    public class HandPresence : MonoBehaviour
    {
        public bool showController = false;
        public InputDeviceCharacteristics controllerCharacteristics;
        public GameObject handModelPrefab;
    
        private InputDevice targetDevice;
        private GameObject spawnedHandModel;
        private Animator handAnimator;

        // Start is called before the first frame update
        private void Start()
        {
            TryInitialize();
        }

        private void TryInitialize()
        {
            List<InputDevice> devices = new List<InputDevice>();

            InputDevices.GetDevicesWithCharacteristics(controllerCharacteristics, devices);

            foreach (var item in devices)
            {
                Debug.Log(item.name + item.characteristics);
            }

            if (devices.Count <= 0) return;
            targetDevice = devices[0];
            spawnedHandModel = Instantiate(handModelPrefab, transform);
            handAnimator = spawnedHandModel.GetComponent<Animator>();
        }

        private void UpdateHandAnimation()
        {
            if(targetDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue))
            {
                handAnimator.SetFloat("Trigger", triggerValue);
            }
            else
            {
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

        // Update is called once per frame
        private void Update()
        {
            if(!targetDevice.isValid)
            {
                TryInitialize();
            }
            else
            {
                if (showController)
                {
                    spawnedHandModel.SetActive(false);
                }
                else
                {
                    spawnedHandModel.SetActive(true);
                    UpdateHandAnimation();
                }
            }
        }
    }
}
