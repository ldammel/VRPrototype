using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyUp : MonoBehaviour
{
    [SerializeField] private float speed;

    void Update()
    {
        transform.Translate(0,speed * Time.deltaTime,0);
    }
}
