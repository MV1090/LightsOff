using Unity.VisualScripting;
using UnityEngine;

public class SFXManager : Singleton<SFXManager>
{
    private AudioSource audioSource;

    [SerializeField] AudioClip clickSound;
    [SerializeField] AudioClip shutterSound; 
    [SerializeField] AudioClip backSound;
    [SerializeField] AudioClip[] light1;
    //[SerializeField] AudioClip light2;
    [SerializeField] AudioClip gameOver;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        GameManager.Instance.OnGameOver += PlayGameOverSound;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StopSound()
    {
        audioSource.Stop();
    }
    public void PlayGameOverSound()
    {
        audioSource.PlayOneShot(gameOver);
    }
    public void PlayBackSound()
    {
        audioSource.PlayOneShot(backSound);
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

    public void PlayShutterSound()
    {
        audioSource.PlayOneShot(shutterSound);
    }

}
