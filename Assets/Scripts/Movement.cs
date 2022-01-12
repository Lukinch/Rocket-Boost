using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {
    [SerializeField] float thrust = 100f;
    [SerializeField] float rotation = 10f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] ParticleSystem upThrustParticles;
    [SerializeField] ParticleSystem leftThrustParticles;
    [SerializeField] ParticleSystem rightThrustParticles;
    
    Rigidbody rb;
    AudioSource audiosource;

    private void Start() {
        rb = GetComponent<Rigidbody>();
        audiosource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust() {
        if (Input.GetKey(KeyCode.Space)){
            UpThrustSoundPartciles();
        }
        else {
            StopUpSoundParticles();
        }
  }

  void ProcessRotation() {
    if (Input.GetKey(KeyCode.A)) {
        ApplyRightRotationAndPartciles();
    } else if (Input.GetKey(KeyCode.D)) {
        ApplyLeftRotationAndPartciles();
    } else {
        StopRotationParticles();
    }
  }

  private void StopUpSoundParticles()
  {
    audiosource.Stop();
    upThrustParticles.Stop();
  }

  private void UpThrustSoundPartciles() {
    rb.AddRelativeForce(Vector3.up * thrust * Time.deltaTime);
    if (!audiosource.isPlaying) {
        audiosource.PlayOneShot(mainEngine);
    }
    if (!upThrustParticles.isPlaying) {
        upThrustParticles.Play();
    }
  }

  private void ApplyRotation(float rotationThisFrame)
  {
      rb.freezeRotation = true;
      transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
      rb.constraints = RigidbodyConstraints.FreezeAll &
                       (~RigidbodyConstraints.FreezeRotationZ &
                       ~RigidbodyConstraints.FreezePositionX &
                       ~RigidbodyConstraints.FreezePositionY);
  }
  
  private void ApplyRightRotationAndPartciles() {
    ApplyRotation(rotation);
    if (!rightThrustParticles.isPlaying) {
        rightThrustParticles.Play();
    }
  }

  private void ApplyLeftRotationAndPartciles() {
    ApplyRotation(-rotation);
    if (!leftThrustParticles.isPlaying) {
        leftThrustParticles.Play();
    }
  }

  private void StopRotationParticles() {
    leftThrustParticles.Stop();
    rightThrustParticles.Stop();
  }
}
