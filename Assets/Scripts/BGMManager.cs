using UnityEngine;

public class BGMManager : MonoBehaviour
{
    public static BGMManager instance;

    [SerializeField] private AudioSource audioSource;

    private void Awake()
    {
        if(instance !=  null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
    }

    public void PlayBGM(AudioClip clip)
    {
        if (audioSource.clip == clip) return;

        audioSource.clip = clip;
        audioSource.loop = true;
        audioSource.Play();
    }

    public void StopBGM()
    {
        audioSource.Stop();
    }
}
