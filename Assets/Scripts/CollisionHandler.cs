using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float sceneDelay = 0.5f;
    [SerializeField] AudioClip crash;
    [SerializeField] AudioClip success;
    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem creashParticles;
    
    DebugKeys debugger;
    Movement momentum;
    AudioSource audioSource;

    int totalScenes;
    bool isTransitioning = false;

    private void Start() {
        momentum = GetComponent<Movement>();
        totalScenes = SceneManager.sceneCountInBuildSettings;
        audioSource = GetComponent<AudioSource>();
        debugger = GetComponent<DebugKeys>();
    }

    void OnCollisionEnter(Collision other) {
        if (isTransitioning || debugger.collissionDisabled) {return;}

        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("You hitted something Friendly >.>");
                break;
            case "Finish":
                Debug.Log("You hitted the Finish line! \\o/");
                StartNextLevelSequence();
                break;
            case "Death":
                Debug.Log("You Died");
                StartCrashSequence();
                break;
            default:
                Debug.Log("I don't know what you bumped into O_o");
                break;
        }
    }


    void StartCrashSequence() {
        creashParticles.Play();
        setupTransition();
        audioSource.PlayOneShot(crash);
        Invoke("RealoadLevel", sceneDelay); // Invoke is bad, uses a string reference to get the method, and is not good in performance
    }

    void StartNextLevelSequence() {
        successParticles.Play();
        setupTransition();
        audioSource.PlayOneShot(success);
        Invoke("LoadNextLevel", sceneDelay); // Invoke is bad, uses a string reference to get the method, and is not good in performance
    }

    void RealoadLevel() {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);
    }

    void LoadNextLevel() {
        int nextScene = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextScene < totalScenes) {
            SceneManager.LoadScene(nextScene);
        } else if (nextScene == totalScenes) {
            SceneManager.LoadScene(0);
        }
    }

    void setupTransition() {
        isTransitioning = true;
        momentum.enabled = false;
        audioSource.Stop();
    }
}
