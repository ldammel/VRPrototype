using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

namespace Movement
{
    public class ContinuousMovement : MonoBehaviour
    {
        [SerializeField] private float speed = 1;
        [SerializeField] private XRNode inputSource;
        [SerializeField] private float gravity = -9.81f;
        [SerializeField] private LayerMask groundLayer;
        [SerializeField] private float additionalHeight = 0.2f;

        private float fallingSpeed;
        private XRRig rig;
        private Vector2 inputAxis;
        private CharacterController character;

        private void Start()
        {
            character = GetComponent<CharacterController>();
            rig = GetComponent<XRRig>();
        }
	
        //Get input values
        private void Update()
        {
            InputDevice device = InputDevices.GetDeviceAtXRNode(inputSource);
            device.TryGetFeatureValue(CommonUsages.primary2DAxis, out inputAxis);
        }

        private void FixedUpdate()
        {
            HeightAndCenter();

            Quaternion headYaw = Quaternion.Euler(0, rig.cameraGameObject.transform.eulerAngles.y, 0);
            Vector3 direction = headYaw *  new Vector3(inputAxis.x, 0, inputAxis.y);

            character.Move(direction * Time.fixedDeltaTime * speed);

            //Gravity
            bool isGrounded = CheckIfGrounded();
            if (isGrounded)
                fallingSpeed = 0;
            else
                fallingSpeed += gravity * Time.fixedDeltaTime;

            character.Move(Vector3.up * fallingSpeed * Time.fixedDeltaTime);
        }

        //Get Height and Center of the Player
        private void HeightAndCenter()
        {
            character.height = rig.cameraInRigSpaceHeight + additionalHeight;
            Vector3 capsuleCenter = transform.InverseTransformPoint(rig.cameraGameObject.transform.position);
            character.center = new Vector3(capsuleCenter.x, character.height/2 + character.skinWidth , capsuleCenter.z);
        }

        //Tells us if we are on ground
        private bool CheckIfGrounded()
        {
            Vector3 rayStart = transform.TransformPoint(character.center);
            float rayLength = character.center.y + 0.01f;
            bool hasHit = Physics.SphereCast(rayStart, character.radius, Vector3.down, out RaycastHit hitInfo, rayLength, groundLayer);
            return hasHit;
        }
    }
}
