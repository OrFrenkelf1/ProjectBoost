using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float mainThrust;
    [SerializeField] float rotationThust;
    [SerializeField] AudioClip mainEngine;

    [SerializeField] ParticleSystem mainEngineParticles;
    [SerializeField] ParticleSystem leftThrusterParticles;
    [SerializeField] ParticleSystem rightThrusterParticles;

    Rigidbody rb;
    AudioSource audioSource;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            StartTruster();
        }
        else
        {
            StopTruster();
        }
    }
    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            LeftThrusterParticlePlay();
        }
        else if (Input.GetKey(KeyCode.D))
        {
            RightThrusterParticlePlay();
        }
        else
        {
            StopSideParticleThrusters();
        }
    }
    
    void StartTruster()
    {
        rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
        if (!audioSource.isPlaying)
            audioSource.PlayOneShot(mainEngine);
        if (!mainEngineParticles.isPlaying)
            mainEngineParticles.Play();
    }
    void StopTruster()
    {
        audioSource.Stop();
        mainEngineParticles.Stop();
    }
    void LeftThrusterParticlePlay()
    {
        ApplyRotation(rotationThust);
        if (!leftThrusterParticles.isPlaying)
        {
            leftThrusterParticles.Play();
        }
    }
    void RightThrusterParticlePlay()
    {
        ApplyRotation(-rotationThust);
        if (!rightThrusterParticles.isPlaying)
        {
            rightThrusterParticles.Play();
        }
    }
    void StopSideParticleThrusters()
    {
        rightThrusterParticles.Stop();
        leftThrusterParticles.Stop();
    }
    private void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true; //Freezing rotation to manualy rotate
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = true;
    }
}

