using UnityEngine;

public class GameplayAudioStarter : MonoBehaviour
{
    private void Start()
    {
        if (AudioManager.instance != null)
        {
            AudioManager.instance.PlayGameplayBGM();
        }
    }
}
