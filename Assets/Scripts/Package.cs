using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Package : MonoBehaviour
{
    AudioSource AudioSource;
    Collider Collider;
    MeshRenderer mRenderer;
    Light Light;

    public bool carryingPackage;

    private void Start()
    {
        AudioSource = GetComponent<AudioSource>();
        Collider = GetComponent<Collider>();
        mRenderer = GetComponent<MeshRenderer>();
        Light = GetComponentInChildren<Light>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        AudioSource.Play();
        Collider.enabled = !Collider.enabled;
        mRenderer.enabled = !mRenderer.enabled;
        Light.enabled = false;
        carryingPackage = !carryingPackage;
    }
}
