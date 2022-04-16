using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coal : MonoBehaviour
{
    public Energy energy;
    private float countdown;
    public TemperatureController temperatureController;
    public float generationSpeed;

    // Start is called before the first frame update
    void Start()
    {
        temperatureController = GameObject.FindWithTag("Temperature Controller").GetComponent<TemperatureController>();
        temperatureController.Pollution++;
        energy = GameObject.FindObjectOfType<Energy>();
    }

    // Update is called once per frame
    void Update()
    {
        countdown -= Time.deltaTime;
        if (countdown <= 0)
        {
            countdown = generationSpeed;
            energy.amount += 2;
        }
    }
}