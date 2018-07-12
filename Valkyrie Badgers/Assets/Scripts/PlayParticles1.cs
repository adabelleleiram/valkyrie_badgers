using UnityEngine;
using System.Collections;

public class PlayParticles1 : MonoBehaviour {

    [SerializeField]
    private ParticleSystem particleObject;

    void Start()
    {
        particleObject = GetComponent<ParticleSystem>();
        particleObject.Stop();
    }
    public void Play () 
    {
        Debug.Log("Play:");
        particleObject.Play();
    }

}