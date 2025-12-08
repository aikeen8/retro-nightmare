using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("Audio Sources (assign the AudioSource components on this GameObject)")]
    public AudioSource bgmSource;
    public AudioSource sfxSource;

    [Header("Clips")]
    public AudioClip gameplayBGM;
    public AudioClip gameOverBGM;
    public AudioClip clickSFX;
    public AudioClip mainMenuBGM;

    [Header("Fade settings")]
    public float fadeDuration = 1f; 

    private Coroutine fadeCoroutine;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        if (bgmSource == null) Debug.LogWarning("AudioManager: bgmSource not assigned.");
        if (sfxSource == null) Debug.LogWarning("AudioManager: sfxSource not assigned.");

        // ⭐ AUTO PLAY BGM BASED ON CURRENT SCENE ⭐
        string currentScene = SceneManager.GetActiveScene().name;

        if (currentScene == "Main Menu")
        {
            // Main Menu BGM
            PlayBGMImmediate(mainMenuBGM, true);
        }
        else
        {
            // Any other scene → Gameplay BGM
            PlayBGMImmediate(gameplayBGM, true);
        }
    }


    public void PlayBGMImmediate(AudioClip clip, bool loop = true)
    {
        if (bgmSource == null || clip == null) return;
        bgmSource.Stop();
        bgmSource.clip = clip;
        bgmSource.loop = loop;
        bgmSource.volume = 1f;
        bgmSource.Play();
    }

   
    public void PlayGameplayBGM()
    {
        if (gameplayBGM == null) return;
        StartFadeToClip(gameplayBGM, true);
    }

    public void PlayGameOverBGM()
    {
        if (gameOverBGM == null) return;
        StartFadeToClip(gameOverBGM, true);
    }
    public void PlayMainMenuBGM()
    {
        if (mainMenuBGM == null) return;
        StartFadeToClip(mainMenuBGM, true);
    }
    public void PlayClick()
    {
        if (sfxSource == null || clickSFX == null) return;
        sfxSource.PlayOneShot(clickSFX);
    }
    private void StartFadeToClip(AudioClip newClip, bool loop)
    {
        if (bgmSource == null) return;

        if (fadeCoroutine != null)
        {
            StopCoroutine(fadeCoroutine);
        }
        fadeCoroutine = StartCoroutine(FadeToClipCoroutine(newClip, loop));
    }

    
    private IEnumerator FadeToClipCoroutine(AudioClip newClip, bool loop)
    {
        float startVolume = bgmSource.volume;
        float t = 0f;

        
        while (t < fadeDuration)
        {
            t += Time.unscaledDeltaTime; 
            bgmSource.volume = Mathf.Lerp(startVolume, 0f, t / Mathf.Max(0.0001f, fadeDuration));
            yield return null;
        }

        bgmSource.Stop();
        bgmSource.clip = newClip;
        bgmSource.loop = loop;
        bgmSource.Play();

      
        t = 0f;
        while (t < fadeDuration)
        {
            t += Time.unscaledDeltaTime;
            bgmSource.volume = Mathf.Lerp(0f, 1f, t / Mathf.Max(0.0001f, fadeDuration));
            yield return null;
        }

        bgmSource.volume = 1f;
        fadeCoroutine = null;
    }
}
