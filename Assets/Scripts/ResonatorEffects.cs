using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResonatorEffects : MonoBehaviour
{
    public Material emissiveMaterial;
    public Renderer objectToChange;
    public float minIntensity;
    public float maxIntensity;
    public float frequency;
    private float time;
    public float lightMin;
    public float lightMax;
    public Light light1;
    public Light light2;

    
    void Start()
    {
        emissiveMaterial = objectToChange.GetComponent<Renderer>().material;
    }


    void Update()
    {
        time += Time.deltaTime;
        float oscilation = Mathf.Sin(time * frequency) * 0.5f + 0.5f;
        float value = Mathf.Lerp(minIntensity, maxIntensity, oscilation);

        float lightoscilation = Mathf.Sin(time * frequency) * 0.5f + 0.5f;
        float lightvalue = Mathf.Lerp(lightMin, lightMax, lightoscilation);


        SetEmissionIntensity(value);
        light1.intensity =lightvalue;
        light2.intensity = lightvalue;
    }

    public void SetEmissionIntensity(float value)
    {
        emissiveMaterial.SetColor("_EmissionColor", Color.magenta * value);
    }
    
}
