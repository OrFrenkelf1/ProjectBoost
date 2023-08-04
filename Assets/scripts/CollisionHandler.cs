using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] AudioClip deathSound;
    [SerializeField] AudioClip succsessSound;

    [SerializeField] ParticleSystem deathParticle;
    [SerializeField] ParticleSystem successParticle;

    float invokeDelay = 2f;

    bool isTransitioning = false;
    bool collisionDisabled = false;

    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    void Update()
    {
        RespondToDebugKeys();
    }
    void RespondToDebugKeys()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadNextLevel();
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            collisionDisabled = !collisionDisabled; // toggle collision
        }
    }
    void OnCollisionEnter(Collision other)
    {
        if (isTransitioning || collisionDisabled)
            return;

        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("This is Okay to hit");
                break;
            case "Finish":
                StartSuccessSequence();
                break;
            case "Fuel":
                Debug.Log("This is fuel");
                break;
            default:
                StartCrashSequence();
                break;
        }
    }
    void StartCrashSequence()
    {
        isTransitioning = true;
        audioSource.Stop();
        deathParticle.Play();
        GetComponent<Movement>().enabled = false;
        audioSource.PlayOneShot(deathSound);
        Invoke("ReloadLevel", invokeDelay);
    }
    void StartSuccessSequence()
    {
        isTransitioning = true;
        audioSource.Stop();
        successParticle.Play();
        GetComponent<Movement>().enabled = false;
        audioSource.PlayOneShot(succsessSound);
        Invoke("LoadNextLevel", invokeDelay);
    }
    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        Debug.Log("heyyy load");
        SceneManager.LoadScene(currentSceneIndex);
    }
    void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex++;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
            nextSceneIndex = 0;
        SceneManager.LoadScene(nextSceneIndex);
    }
}
