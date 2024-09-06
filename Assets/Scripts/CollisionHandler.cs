using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    // PARAMETERS - for tuning, typically set in the editor
    // CACHE - e.g. references for readability or speed
    // STATE - private instance (member) variables

    [SerializeField] float levelLoadDelay;
    [SerializeField] AudioClip crashingSFX;
    [SerializeField] AudioClip levelSuccessSFX;
    [SerializeField] ParticleSystem crashingParticle;
    [SerializeField] ParticleSystem levelSuccessParticle;

    public Package package;
    AudioSource audioSource;
    public GameObject packageChild;

    public bool isTransitioning = false;
    bool collisionDisabled = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    void Update()
    {
        RespondToDebugKeys();
        ShowPackage();
    }

    private void ShowPackage()
    {
        if (package.carryingPackage == true)
        {
            packageChild.GetComponent<MeshRenderer>().enabled = true;
            packageChild.GetComponent<Collider>().enabled = true;
        }
        else
        {
            packageChild.GetComponent<MeshRenderer>().enabled = false;
            packageChild.GetComponent<Collider>().enabled = false;
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (isTransitioning || collisionDisabled) { return; }

        switch (other.gameObject.tag)
        {
            case "Friendly":
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
    private void RespondToDebugKeys()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            Debug.Log("Cheat code used! You jumped to the next level");
            LoadNextLevel();
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            collisionDisabled = !collisionDisabled; //toggle collision
            if (collisionDisabled)
            {
                Debug.Log("Cheat code used! Now you are invulnerable");
            }
            else
            {
                Debug.Log("You are not invulnerable anymore :(");
            }
        }
    }
    void StartCrashSequence()
    {
        isTransitioning = true;
        audioSource.Stop();
        crashingParticle.Play();
        audioSource.PlayOneShot(crashingSFX); 
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", levelLoadDelay);

    }

    void NextLevelSequence()
    {
        isTransitioning = true;
        audioSource.Stop();
        levelSuccessParticle.Play();
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
