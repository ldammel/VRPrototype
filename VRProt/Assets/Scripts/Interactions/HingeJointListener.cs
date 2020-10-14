using UnityEngine;
using UnityEngine.Events;

namespace Interactions
{
    public class HingeJointListener : MonoBehaviour
    {
        //angle threshold to trigger if we reached limit
        public float angleBetweenThreshold = 1f;
        //State of the hinge joint : either reached min or max or none if in between
        public HingeJointState hingeJointState = HingeJointState.None;

        //Event called on min reached
        public UnityEvent onMinLimitReached;
        //Event called on max reached
        public UnityEvent onMaxLimitReached;

        public enum HingeJointState { Min,Max,None}
        private HingeJoint hinge;

        private void Start()
        {
            hinge = GetComponent<HingeJoint>();
        }

        private void FixedUpdate()
        {
            var angleWithMinLimit = Mathf.Abs(hinge.angle - hinge.limits.min);
            var angleWithMaxLimit = Mathf.Abs(hinge.angle - hinge.limits.max);

            //Reached Min
            if(angleWithMinLimit < angleBetweenThreshold)
            {
                if (hingeJointState != HingeJointState.Min)
                    onMinLimitReached.Invoke();

                hingeJointState = HingeJointState.Min;
            }
            //Reached Max
            else if (angleWithMaxLimit < angleBetweenThreshold)
            {
                if (hingeJointState != HingeJointState.Max)
                    onMaxLimitReached.Invoke();

                hingeJointState = HingeJointState.Max;
            }
            //No Limit reached
            else
            {
                hingeJointState = HingeJointState.None;
            }
        }
    }
}
