using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorMovement : MonoBehaviour
{
    public float time_delay, time_delaySound;

    public AudioClip main;

    void Start()
    {
        StartCoroutine(Animate());
        StartCoroutine(AddSound());

    }

    void Update()
    {
       
    }

    IEnumerator Animate()
    {
        yield return new WaitForSeconds(time_delay);
        GetComponent<Animator>().enabled = true;
    }

    IEnumerator AddSound()
    {
        yield return new WaitForSeconds(time_delaySound);
        GetComponent<AudioSource>().clip = main;
        GetComponent<AudioSource>().Play();
    }
}
