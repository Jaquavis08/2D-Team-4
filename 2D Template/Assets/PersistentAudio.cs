using UnityEngine;

public class PersistentAudio : MonoBehaviour
{
    private static PersistentAudio instance;

    void Awake()
    {
        // Ensure only one instance of the audio source exists
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Prevent this GameObject from being destroyed
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate audio sources
        }
    }
}