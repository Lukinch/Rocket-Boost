using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    Vector3 startingPosition;
    [SerializeField] Vector3 movementVector;
    [SerializeField] [Range(0,1)] float movementFactor;
    [SerializeField] [Range(2,20)] float period = 2f;

    const float tau = Mathf.PI *2; // constant value of 6.283

    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        float cycles = Time.time / period; // Continually growing over time

        float rawSinWave = Mathf.Sin(cycles * tau); // Sin wave goes from -1 to 1

        movementFactor = (rawSinWave + 1f) / 2f; // Recalculated to go from 0 to 1.

        Vector3 offset = movementVector * movementFactor;
        transform.position =  startingPosition + offset;
    }
}
