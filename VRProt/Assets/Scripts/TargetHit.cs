using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class TargetHit : MonoBehaviour
{
    public UnityEvent OnHit;
    [SerializeField] private GameObject sphere;

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("Weapon"))
        {
            OnHit.Invoke();
            var obj = Instantiate(sphere, transform);
            Destroy(obj,4);
        }
    }
}
