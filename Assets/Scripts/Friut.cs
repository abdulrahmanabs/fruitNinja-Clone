using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour
{

    private enum FriutType
    {
        melon, kiwi, apple, orange
    }
    [SerializeField] FriutType friut;
    [SerializeField] GameObject wholeFruit;
    [SerializeField] GameObject slicedFruit;
    [SerializeField] float minSliceForce = 1.0f;
    ParticleSystem particle;
    [SerializeField] Material insideMaterial;

    private bool isSlicable = true;
    private Rigidbody wholeRigidbody;

    private int score;

    private void Awake()
    {
        wholeRigidbody = wholeFruit.GetComponentInParent<Rigidbody>();
        particle = GetComponentInChildren<ParticleSystem>();

        insideMaterial = transform.GetChild(1).GetChild(0).GetComponent<MeshRenderer>().materials[1];
        GetComponentInChildren<ParticleSystemRenderer>().material = insideMaterial;

        SetFruitScore();
    }

    private void SetFruitScore()
    {
        switch (friut)
        {
            case FriutType.melon:
                score = 100;
                break;
            case FriutType.kiwi:
                score = 40;
                break;
            case FriutType.apple: score = 50; break;
            case FriutType.orange: score = 80; break;

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isSlicable && other.CompareTag("Player"))
        {
            Blade blade = other.GetComponent<Blade>();
            Slice(blade.direction, blade.transform.position, blade.force);
        }
        else if(other.gameObject.CompareTag("Fall Area"))
        {
            if(!isSlicable)
            return; 
            GameManager.instance.IncreaseFallingFruits();
            gameObject.SetActive(false);
        }
    }

    private void Slice(Vector3 direction, Vector3 position, float force)
    {
        if (!isSlicable)
            return;


        wholeFruit.SetActive(false);
        slicedFruit.SetActive(true);
        wholeRigidbody.isKinematic = true;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        slicedFruit.transform.rotation = Quaternion.Euler(0, 0, angle);

        Rigidbody[] slicedRigidbodies = slicedFruit.GetComponentsInChildren<Rigidbody>();

        foreach (Rigidbody rb in slicedRigidbodies)
        {
            rb.velocity = wholeRigidbody.velocity;
            rb.AddForceAtPosition(direction.normalized * force, position, ForceMode.Impulse);
        }
        particle.Play();
        print("direction : "+direction);
        print("angle : "+angle);
        print("Position : "+position);
        isSlicable = false;
        SoundManager.instance.PlaySound(SoundManager.sounds.slice);
        GameManager.instance.IncreaseScore(score);
        
    }
}