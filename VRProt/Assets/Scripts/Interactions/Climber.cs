using Movement;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

namespace Interactions
{
    public class Climber : MonoBehaviour
    {
        private CharacterController character;
        public static XRController ClimbingHand;
        private ContinuousMovement continuousMovement;
        
        private void Start()
        {
            character = GetComponent<CharacterController>();
            continuousMovement = GetComponent<ContinuousMovement>();
        }
        
        private void FixedUpdate()
        {
            if(ClimbingHand)
            {
                continuousMovement.enabled = false;
                Climb();
            }
            else
            {
                continuousMovement.enabled = true;
            }
        }

        //Climbing Computations
        private void Climb()
        {
            InputDevices.GetDeviceAtXRNode(ClimbingHand.controllerNode).TryGetFeatureValue(CommonUsages.deviceVelocity, out Vector3 velocity);

            character.Move(transform.rotation * -velocity * Time.fixedDeltaTime);
        }
    }
}
