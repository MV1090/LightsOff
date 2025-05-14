using System.Collections;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioSource menuMusic;
    public AudioSource gameMusic;
    public AudioSource loopMusic;
    public AudioSource gameOverMusic;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        foreach(LightObject light in LightManager.Instance.allLightObjects)
        {
            light.OnGameStart += StartGameMusic;
        }

        GameManager.Instance.OnGameOver += GameOver;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void StartGameMusic()
    {
        menuMusic.Stop();
        gameMusic.Play();
        StartCoroutine(PlayLoopAfterDelay(gameMusic.clip.length));
    }
    private IEnumerator PlayLoopAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        loopMusic.loop = true;
        loopMusic.Play();
    }

    void GameOver()
    {
        if(gameMusic.isPlaying)  
            gameMusic.Stop();

        if(loopMusic.isPlaying)
            loopMusic.Stop();

        gameOverMusic.Play();
        StartCoroutine(PlayMenuMusicDelay(gameOverMusic.clip.length));
    }
    private IEnumerator PlayMenuMusicDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        menuMusic.loop = true;
        menuMusic.Play();
    }

}
