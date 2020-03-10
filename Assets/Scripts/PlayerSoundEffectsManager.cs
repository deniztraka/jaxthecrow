using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundEffectsManager : MonoBehaviour
{
    private AudioSource source;
    // Start is called before the first frame update
    public AudioClip Shout;
    public AudioClip Movement;
    public AudioClip Jump;
    public AudioClip Land;
    public AudioClip Flying;
    public AudioClip Eating;

    void Start()
    {
        source = GetComponent<AudioSource>();
    }


    public void Play(string clipName, bool loop)
    {
        switch (clipName)
        {
            case "eating":
                if (loop)
                {
                    source.clip = Eating;
                    source.loop = true;
                    if (!source.isPlaying)
                    {   
                        source.Play();
                    }
                }
                else
                {
                    source.PlayOneShot(Eating);
                }

                break;
            case "shout":
                if (loop)
                {
                    source.clip = Shout;
                    source.loop = true;
                    if (!source.isPlaying)
                    {   
                        source.Play();
                    }
                }
                else
                {
                    source.PlayOneShot(Shout);
                }

                break;
            case "movement":
                if (loop)
                {
                    source.clip = Movement;
                    source.loop = true;
                    if (!source.isPlaying)
                    {   
                        source.Play();
                    }
                }
                else
                {
                    source.PlayOneShot(Movement);
                }
                break;
            case "jump":
                if (loop)
                {
                    source.clip = Jump;
                    source.loop = true;
                    if (!source.isPlaying)
                    {   
                        source.Play();
                    }
                }
                else
                {
                    source.PlayOneShot(Jump);
                }
                break;
            case "land":
                if (loop)
                {
                    source.clip = Land;
                    source.loop = true;
                    if (!source.isPlaying)
                    {   
                        source.Play();
                    }
                }
                else
                {
                    source.PlayOneShot(Land);
                }
                break;
            case "flying":
                if (loop)
                {
                    source.clip = Flying;
                    source.loop = true;
                    if (!source.isPlaying)
                    {   
                        source.Play();
                    }
                }
                else
                {
                    source.PlayOneShot(Flying);
                }
                break;
            default:
                source.clip = null;
                source.loop = false;
                break;
        }
    }
}
