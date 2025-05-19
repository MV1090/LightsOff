using Unity.VisualScripting;
using UnityEngine;

public class SFXManager : Singleton<SFXManager>
{
    private AudioSource audioSource;

    [SerializeField] AudioClip clickSound;
    [SerializeField] AudioClip shutterSound; 
    [SerializeField] AudioClip buttonSound;
    [SerializeField] AudioClip[] light1;    
    [SerializeField] AudioClip[] errorSound;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        //GameManager.Instance.OnGameOver += PlayGameOverSound;
    }

    public void StopSound()
    {
        audioSource.Stop();
    }
    //public void PlayGameOverSound()
    //{
    //    audioSource.PlayOneShot(gameOver);
    //}
    public void PlayBackSound()
    {
        audioSource.PlayOneShot(buttonSound);
    }

    public void PlayClickedSound()
    {
        int randNum = Random.Range(0, light1.Length);

        audioSource.PlayOneShot(light1[randNum]);

        //if (randNum % 2 == 0)
        //    audioSource.PlayOneShot(light1);
        //else
        //    audioSource.PlayOneShot(light2);
    }

    public void PlayErrorSound()
    {
        int randNum = Random.Range(0, errorSound.Length);

        if (randNum % 2 == 0)
            audioSource.PlayOneShot(errorSound[randNum]);
        else
            audioSource.PlayOneShot(errorSound[randNum]);
    }

    public void PlayShutterSound()
    {
        audioSource.PlayOneShot(shutterSound);
    }

}
