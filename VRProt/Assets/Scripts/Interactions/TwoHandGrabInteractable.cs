using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace Interactions
{
    public class TwoHandGrabInteractable : XRGrabInteractable
    {
        public List<XRSimpleInteractable> secondHandGrabPoints = new List<XRSimpleInteractable>();
        private XRBaseInteractor secondInteractor;
        private Quaternion attachInitialRotation;
        public enum TwoHandRotationType { None, First, Second };
        public TwoHandRotationType twoHandRotationType;
        public bool snapToSecondHand = true;
        private Quaternion initialRotationOffset;

        //Set up listeners on the grab points
        private void Start()
        {
            foreach (var item in secondHandGrabPoints)
            {
                item.onSelectEnter.AddListener(OnSecondHandGrab);
                item.onSelectExit.AddListener(OnSecondHandRelease);
            }
        }

        //If its grabbed with the second hand, calculate the rotation
        public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
        {
            if (secondInteractor && selectingInteractor)
            {
                //Compute the rotation 
                if(snapToSecondHand)
                    selectingInteractor.attachTransform.rotation = GetTwoHandRotation();
                else
                    selectingInteractor.attachTransform.rotation = GetTwoHandRotation() * initialRotationOffset;
            }
            base.ProcessInteractable(updatePhase);
        }

        //Set the rotation based on the rotation type
        private Quaternion GetTwoHandRotation()
        {
            Quaternion targetRotation;
            if (twoHandRotationType == TwoHandRotationType.None)
            {
                targetRotation = Quaternion.LookRotation(secondInteractor.attachTransform.position - selectingInteractor.attachTransform.position); 
            }
            else if (twoHandRotationType == TwoHandRotationType.First)
            {
                targetRotation = Quaternion.LookRotation(secondInteractor.attachTransform.position - selectingInteractor.attachTransform.position, selectingInteractor.attachTransform.up);
            }
            else
            {
                targetRotation = Quaternion.LookRotation(secondInteractor.attachTransform.position - selectingInteractor.attachTransform.position, secondInteractor.attachTransform.up);
            }

            return targetRotation;
        }

        private void OnSecondHandGrab(XRBaseInteractor interactor)
        {
            secondInteractor = interactor;
            initialRotationOffset = Quaternion.Inverse(GetTwoHandRotation()) * selectingInteractor.attachTransform.rotation;
        }

        private void OnSecondHandRelease(XRBaseInteractor interactor)
        {
            secondInteractor = null;
        }

        protected override void OnSelectEnter(XRBaseInteractor interactor)
        {
            base.OnSelectEnter(interactor);
            attachInitialRotation = interactor.attachTransform.localRotation;
        }

        protected override void OnSelectExit(XRBaseInteractor interactor)
        {
            base.OnSelectExit(interactor);
            secondInteractor = null;
            interactor.attachTransform.localRotation = attachInitialRotation;
        }

        public override bool IsSelectableBy(XRBaseInteractor interactor)
        {
            var isAlreadyGrabbed = selectingInteractor && !interactor.Equals(selectingInteractor);
            return base.IsSelectableBy(interactor) && !isAlreadyGrabbed;
        }
    }
}
