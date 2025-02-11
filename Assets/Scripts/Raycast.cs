using UnityEngine;
using System;
using System.Collections;
using UnityEngine.Audio;

/// <summary>
/// Handles raycasting to detect objects with an AudioSource and smoothly transitions 
/// audio between the focused and ambient AudioMixerGroups.
/// </summary>
public class Raycast : MonoBehaviour
{
    #region Variables
    [Header("Audio Mixer Group References")]
    [SerializeField] AudioMixerGroup focusedGroup; // Focused audio group
    [SerializeField] AudioMixerGroup ambientGroup; // Ambient audio group

    [SerializeField] float transitionDuration = 1.0f; // Time for transitions

    private AudioSource currentAudioSource; // Track the currently focused audio source
    private AudioSource previousAudioSource; // Track the previous audio source

    #endregion
    #region Unity Methods

    /// <summary>
    /// Performs a raycast from the center of the screen to detect objects with an AudioSource.
    /// Transitions audio between focused and ambient mixer groups accordingly.
    /// </summary>
    void Update()
    {
        // Create a ray from the center of the screen
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));

        // Perform the raycast
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            // Check if the hit object has an AudioSource
            AudioSource newAudioSource = hit.collider.GetComponent<AudioSource>();

            if (newAudioSource != null && newAudioSource != currentAudioSource)
            {
                // Handle transition
                if (currentAudioSource != null)
                {
                    previousAudioSource = currentAudioSource;
                    StartCoroutine(FadeOut(previousAudioSource));
                }

                currentAudioSource = newAudioSource;
                StartCoroutine(FadeIn(currentAudioSource));
            }
        }
        else if (currentAudioSource != null)
        {
            // If no longer hitting the target, revert back to ambient
            previousAudioSource = currentAudioSource;
            StartCoroutine(FadeOut(previousAudioSource));
            currentAudioSource = null;
        }
    }
    #endregion
    #region Custom Methods

    /// <summary>
    /// Smoothly fades in an audio source by increasing its volume over time.
    /// </summary>
    /// <param name="audioSource">The AudioSource to fade in.</param>
    IEnumerator FadeIn(AudioSource audioSource)
    {
        if (audioSource == null) yield break;

        audioSource.outputAudioMixerGroup = focusedGroup;
        float elapsedTime = 0f;
        float startVolume = 0f;
        audioSource.volume = startVolume;

        while (elapsedTime < transitionDuration)
        {
            audioSource.volume = Mathf.Lerp(startVolume, 1f, elapsedTime / transitionDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        audioSource.volume = 1f;
    }

    /// <summary>
    /// Smoothly fades out an audio source by decreasing its volume over time.
    /// After reaching zero, it switches to the ambient audio mixer group.
    /// </summary>
    /// <param name="audioSource">The AudioSource to fade out.</param>
    IEnumerator FadeOut(AudioSource audioSource)
    {
        if (audioSource == null) yield break;

        float elapsedTime = 0f;
        float startVolume = audioSource.volume;

        while (elapsedTime < transitionDuration)
        {
            audioSource.volume = Mathf.Lerp(startVolume, 0f, elapsedTime / transitionDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        audioSource.volume = 0f;
        audioSource.outputAudioMixerGroup = ambientGroup;

        // Fade back to full volume in ambient group
        StartCoroutine(FadeToFullVolume(audioSource));
    }

    /// <summary>
    /// Fades an audio source back to full volume after switching to the ambient group.
    /// </summary>
    /// <param name="audioSource">The AudioSource to restore volume.</param>
    IEnumerator FadeToFullVolume(AudioSource audioSource)
    {
        if (audioSource == null) yield break;

        float elapsedTime = 0f;
        float startVolume = 0f;
        audioSource.volume = startVolume;

        while (elapsedTime < transitionDuration)
        {
            audioSource.volume = Mathf.Lerp(startVolume, 1f, elapsedTime / transitionDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        audioSource.volume = 1f;
    }
    #endregion
}