using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

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
    public Transform player;
    public float ppEffectMaxDistance;
    private float distance;
    private float mappedValue;
    public Volume volume;
    FilmGrain filmGrain;
    ChromaticAberration chromaticAberration;
    public Transform anchorPoint;
    
    void Start()
    {
        emissiveMaterial = objectToChange.GetComponent<Renderer>().material;
        volume.profile.TryGet<FilmGrain>(out filmGrain);
        volume.profile.TryGet<ChromaticAberration>(out chromaticAberration);
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

    private void FixedUpdate()
    {
        distance = Vector3.Distance(player.position, anchorPoint.position);
        Debug.Log(distance);
        mappedValue = Mathf.InverseLerp(0f, ppEffectMaxDistance, distance);
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
