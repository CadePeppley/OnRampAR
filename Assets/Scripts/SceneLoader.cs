using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    // Call this from your �Extend the frame� button
    public void LoadExtendFrame()
    {
        // Both Extend Frame and Animation Mode use MainScene
        SceneManager.LoadScene("BeyondTheFrame");
    }

    // Call this from your �Animation Mode� button
    public void LoadAnimationMode()
    {
        SceneManager.LoadScene("MainScene");
    }

    // Call this from your �Sound Mode� button
    public void LoadSoundMode()
    {
        SceneManager.LoadScene("SoundScapes");
    }
}
