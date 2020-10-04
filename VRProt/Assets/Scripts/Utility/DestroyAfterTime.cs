using System;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{

    [SerializeField] private float delay;

    private void Start()
    {
        Destroy(this.gameObject, delay);
    }
}
