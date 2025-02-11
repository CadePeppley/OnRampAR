using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages a list of GameObjects and controls their AudioSources.
/// Provides public methods to play or stop all AudioSources in the list.
/// </summary>
public class ImageAudioToggle : MonoBehaviour
{
    #region Variables

    [Header("Drab and drop all selected audio zone object onto this list using inspector lock")]
    [SerializeField] List<GameObject> audioObjects = new List<GameObject>(); // List of GameObjects containing AudioSources

    #endregion
    #region Custom Methods

    /// <summary>
    /// Loops through the list and plays the AudioSource on each GameObject if available.
    /// </summary>
    public void PlayAllAudio()
    {
        foreach (GameObject obj in audioObjects)
        {
            if (obj != null)
            {
                AudioSource audioSource = obj.GetComponent<AudioSource>();
                if (audioSource != null && !audioSource.isPlaying)
                {
                    audioSource.Play();
                }
            }
        }
    }

    /// <summary>
    /// Loops through the list and stops the AudioSource on each GameObject if available.
    /// </summary>
    public void StopAllAudio()
    {
        foreach (GameObject obj in audioObjects)
        {
            if (obj != null)
            {
                AudioSource audioSource = obj.GetComponent<AudioSource>();
                if (audioSource != null && audioSource.isPlaying)
                {
                    audioSource.Stop();
                }
            }
        }
    }

    #endregion
}
