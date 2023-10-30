using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{

    AudioSource audio;
    [SerializeField] AudioClip[] Clips;
    public static SoundManager instance;

    public void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
        audio = GetComponent<AudioSource>();
    }

    public enum sounds
    {
        hit, jumpSound, Score
    }
    // Start is called before the first frame update
 


    public void PlaySound(sounds sound)
    {
        audio.PlayOneShot(Clips[(int)sound]);


    }
}
