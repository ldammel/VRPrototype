using UnityEngine;

namespace Utility
{
    public class DestroyAfterTime : MonoBehaviour
    {

        [SerializeField] private float delay;

        private void Start()
        {
            Destroy(this.gameObject, delay);
        }
    }
}
