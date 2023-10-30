using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Friut : MonoBehaviour
{
    [SerializeField]GameObject whoelFriut,slicedFruit;
    
    bool slicable=true;
    Rigidbody wholeRb;
    void Awake()
    {
        wholeRb = GetComponent<Rigidbody>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            print("Jointed");
            Blade blade = other.GetComponent<Blade>();
            Slice(blade.direction, blade.transform.position, blade.force);
        }
    }

    private void Slice(Vector3 direction, Vector3 position, float force)
    {
        if(!slicable)
        return;
        whoelFriut.SetActive(false);
        slicedFruit.SetActive(true);
        wholeRb.isKinematic=true;
        float angle = Mathf.Atan2(direction.y, direction.y) * Mathf.Rad2Deg;
        slicedFruit.transform.rotation = Quaternion.Euler(0, 0, angle);
        Rigidbody[] rbs = slicedFruit.GetComponentsInChildren<Rigidbody>();

        foreach (var rb in rbs)
        {
            rb.velocity = wholeRb.velocity;
            rb.AddForceAtPosition(direction * force, position, ForceMode.Impulse);

        }
        slicable=false;
    }
}
