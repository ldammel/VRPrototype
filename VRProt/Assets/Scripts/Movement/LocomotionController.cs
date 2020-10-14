using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace Movement
{
    public class LocomotionController : MonoBehaviour
    {
        public XRController leftTeleportRay;
        public XRController rightTeleportRay;
        public InputHelpers.Button teleportActivationButton;
        public float activationThreshold = 0.1f;

        public XRRayInteractor leftInteractorRay;
        public XRRayInteractor rightInteractorRay;

        public bool EnableLeftTeleport { get; set; } = true;
        public bool EnableRightTeleport { get; set; } = true;

        private void Update()
        {
            Vector3 pos = new Vector3();
            Vector3 norm = new Vector3();
            int index = 0;
            bool validTarget = false;

            if(leftTeleportRay)
            {
                bool isLeftInteractorRayHovering = leftInteractorRay.TryGetHitInfo(ref pos, ref norm, ref index, ref validTarget);
                leftTeleportRay.gameObject.SetActive(EnableLeftTeleport && CheckIfActivated(leftTeleportRay) && !isLeftInteractorRayHovering);
            }

            if (rightTeleportRay)
            {
                bool isRightInteractorRayHovering = rightInteractorRay.TryGetHitInfo(ref pos, ref norm, ref index, ref validTarget);
                rightTeleportRay.gameObject.SetActive(EnableRightTeleport && CheckIfActivated(rightTeleportRay) && !isRightInteractorRayHovering);
            }
        }

        private bool CheckIfActivated(XRController controller)
        {
            controller.inputDevice.IsPressed(teleportActivationButton, out bool isActivated, activationThreshold);
            return isActivated;
        }
    }
}
