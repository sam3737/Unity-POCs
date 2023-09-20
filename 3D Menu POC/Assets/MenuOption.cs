using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuOption : MonoBehaviour
{
    public string optionName;

    public AudioClip myAudioClip;
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.clip = myAudioClip;
    }

    void OnMouseDown()
    {
        Debug.Log(optionName);
    }

    void OnMouseEnter()
    {
        audioSource.Play();
        transform.Rotate(new Vector3(0, 10, 0));
    }

    void OnMouseExit()
    {
        transform.Rotate(new Vector3(0, -10, 0));
    }
}
