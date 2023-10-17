using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioClip pickupSound;
    [SerializeField] AudioClip scoringSound;

    AudioSource m_AudioSource;
    // Start is called before the first frame update
    void Start()
    {
        m_AudioSource = GetComponent<AudioSource>();
    }

    public void PlayPickUpSound()
    {
        m_AudioSource.PlayOneShot(pickupSound);
    }

    public void PlayScoringSound()
    {
        m_AudioSource.PlayOneShot(scoringSound);
    }
}
