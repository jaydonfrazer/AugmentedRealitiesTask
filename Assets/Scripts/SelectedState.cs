using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedState : MonoBehaviour
{
    public Material selectedMaterial; // Creates a slot in the editor for the selected material
    private Material originalMaterial; //Stores the original material
    public AudioClip audioClip; //Creates a slot in the editor for a sound file
    private AudioSource audioSource; //Creates audio playback functionality

    // Start is called before the first frame update
    void Start()
    {
        // setup audio playback
        audioSource = gameObject.AddComponent(typeof(AudioSource)) as AudioSource;
        audioSource.clip = audioClip;

        //Store reference to current material
        originalMaterial = gameObject.GetComponent<MeshRenderer>().material;
    }

    // Public methods that will be exposed to the Event Trigger System
    public void playAudio()
    {
        audioSource.Play();
    }
    public void setSelectedMaterial()
    {
        gameObject.GetComponent<MeshRenderer>().material = selectedMaterial;
    }
    public void setOriginalMaterial()
    {
        gameObject.GetComponent<MeshRenderer>().material = originalMaterial;
    }
}
