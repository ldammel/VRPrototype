using UnityEngine;

namespace Utility
{
    public class FlyUp : MonoBehaviour
    {
        [SerializeField] private float speed;

        private void Update()
        {
            transform.Translate(0,speed * Time.deltaTime,0);
        }
    }
}
