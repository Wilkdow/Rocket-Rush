using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    //PARAMETERS
    //CACHÉ
    //STATE
    [SerializeField] float levelLoadDelay;
    [SerializeField] AudioClip crashingSFX;
    [SerializeField] AudioClip levelSuccessSFX;

    AudioSource audioSource;

    public bool isTransitioning = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    void OnCollisionEnter(Collision other)
    {
        if (!isTransitioning)
        {
            switch (other.gameObject.tag)
            {
                case "Friendly":
                    Debug.Log("You bumped into a friendly object");
                    break;

                case "Finish":
                    Debug.Log("You finished the level!");
                    NextLevelSequence();
                    break;

                default:
                    Debug.Log("You crashed!");
                    StartCrashSequence();
                    break;
            }
        }
    }

    void StartCrashSequence()
    {
        //add SFX and particle effects upon crash
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(crashingSFX); 
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", levelLoadDelay);
    }

    void NextLevelSequence()
    {
        //add SFX and particle effects upon crash
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(levelSuccessSFX);
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", levelLoadDelay);

    }

    void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex != SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            SceneManager.LoadScene(0);
        }
    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
