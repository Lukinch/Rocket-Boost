using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DebugKeys : MonoBehaviour
{
    Collider collision;
    int totalScenes;

    public bool collissionDisabled = false;

    // Start is called before the first frame update
    void Start()
    {
        totalScenes = SceneManager.sceneCountInBuildSettings;
        collision = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update() {
        onLPressed();
        onCPressed();
    }

    private void onLPressed() {
        if (Input.GetKeyDown(KeyCode.L)) {
            LoadNextLevel();
        }
    }

    private void onCPressed() {
        if (Input.GetKeyDown(KeyCode.C)) {
            turnCollisions();
        }
    }


    private void LoadNextLevel() {
        int nextScene = SceneManager.GetActiveScene().buildIndex + 1;
            if (nextScene < totalScenes) {
            SceneManager.LoadScene(nextScene);
        } else if (nextScene == totalScenes) {
            SceneManager.LoadScene(0);
        }
    }
    
    private void turnCollisions() {
        collissionDisabled = !collissionDisabled;
    }

}
