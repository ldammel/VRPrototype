using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace Interactions
{
    public class XROffsetGrabInteractable : XRGrabInteractable
    {
        private Vector3 initialAttachLocalPos;
        private Quaternion initialAttachLocalRot;
        
        private void Start()
        {
            //Create attach point
            if(!attachTransform)
            {
                var grab = new GameObject("Grab Pivot");
                grab.transform.SetParent(transform, false);
                attachTransform = grab.transform;
            }

            initialAttachLocalPos = attachTransform.localPosition;
            initialAttachLocalRot = attachTransform.localRotation;
        }

        protected override void OnSelectEnter(XRBaseInteractor interactor)
        {
            if(interactor is XRDirectInteractor)
            {
                attachTransform.position = interactor.transform.position;
                attachTransform.rotation = interactor.transform.rotation;
            }
            else
            {
                attachTransform.localPosition = initialAttachLocalPos;
                attachTransform.localRotation = initialAttachLocalRot;
            }

            base.OnSelectEnter(interactor);
        }
    }
}
