using UnityEngine;

public class SFXManager : Singleton<SFXManager>
{
    private AudioSource audioSource;

    [SerializeField] AudioClip clickSound;
    [SerializeField] AudioClip shutterSound;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StopSound()
    {
        audioSource.Stop();
    }

    public void PlayClickedSound()
    {
        audioSource.PlayOneShot(clickSound);
    }

    public void PlayShutterSound()
    {
        audioSource.PlayOneShot(shutterSound);
    }

}
