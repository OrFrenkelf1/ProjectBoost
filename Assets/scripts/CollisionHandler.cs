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

    AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    void OnCollisionEnter(Collision other)
    {
        if (isTransitioning)
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

        void StartCrashSequence()
        {
            isTransitioning = true;
            audioSource.Stop();
            GetComponent<Movement>().enabled = false;
            audioSource.PlayOneShot(deathSound);
            deathParticle.Play();
            Invoke("ReloadLevel", invokeDelay);
        }
        void StartSuccessSequence()
        {
            isTransitioning = true;
            audioSource.Stop();
            GetComponent<Movement>().enabled = false;
            audioSource.PlayOneShot(succsessSound);
            successParticle.Play();
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
}
