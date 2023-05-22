using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class ResonatorEffects : MonoBehaviour
{
    [Header("Pulsating Effect")]
    public float frequency;

    [Header("Core Pulsating")]
    public Material emissiveMaterial;
    public Renderer innerSphere;
    public float minEmissionIntensity;
    public float maxEmissionIntensity;

    [Header("Light Pulsating")]
    public float minLightIntensity;
    public float maxLightIntensity;
    public Light light1;
    public Light light2;

    [Header("Post Processing")]
    public float postProcessingStartDistance;
    private float distance;
    private float mappedValue;
    public Volume volume;
    FilmGrain filmGrain;
    ChromaticAberration chromaticAberration;

    [Header("General")]
    public Transform resonatorPoint;
    public Transform player;
    private float time;
    
    void Start()
    {
        emissiveMaterial = innerSphere.GetComponent<Renderer>().material;
        volume.profile.TryGet<FilmGrain>(out filmGrain);
        volume.profile.TryGet<ChromaticAberration>(out chromaticAberration);
    }


    void Update()
    {
        time += Time.deltaTime;
        float oscilation = Mathf.Sin(time * frequency) * 0.5f + 0.5f;
        float value = Mathf.Lerp(minEmissionIntensity, maxEmissionIntensity, oscilation);

        float lightoscilation = Mathf.Sin(time * frequency) * 0.5f + 0.5f;
        float lightvalue = Mathf.Lerp(minLightIntensity, maxLightIntensity, lightoscilation);


        SetEmissionIntensity(value);
        light1.intensity =lightvalue;
        light2.intensity = lightvalue;

        
    }

    private void FixedUpdate()
    {
        distance = Vector3.Distance(player.position, resonatorPoint.position);
        Debug.Log(distance);
        mappedValue = Mathf.InverseLerp(0f, postProcessingStartDistance, distance);
        mappedValue = Mathf.Clamp01(mappedValue);
        mappedValue = 1f - mappedValue;
        filmGrain.intensity.value = mappedValue;
        chromaticAberration.intensity.value = mappedValue / 2;
    }

    public void SetEmissionIntensity(float value)
    {
        emissiveMaterial.SetColor("_EmissionColor", Color.magenta * value);
    }
    
}
