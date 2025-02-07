using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
public class CollisionHandler : MonoBehaviour
{

    [SerializeField] float delay = 2f;

    [SerializeField] AudioClip VictorySFX;
    [SerializeField] AudioClip DefeatSFX;
    [SerializeField] ParticleSystem VictoryParticles;
    [SerializeField] ParticleSystem DefeatParticles;
    AudioSource audioSource;


    bool isControllable = true;
    bool isCollidable = true;
 
    void Start(){
        audioSource = GetComponent<AudioSource>();
    }

    private void Update() {
        RespondToDebugKeys();
    }

    
    private void OnCollisionEnter(Collision other) {
        if(!isControllable || !isCollidable) {return;}
        switch(other.gameObject.tag){
            case "Friendly":
                break;
            case "Finish":
                LoadingNextLevel();
                break;
            case "Fuel":
                break;
            default:
                Crash();
                break;
        }
    }

 
    void ReloadLevel(){
        
        int activeLevel = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(activeLevel);
    }

    void Crash(){
        isControllable = false;
        audioSource.Stop();
        audioSource.PlayOneShot(DefeatSFX);
        DefeatParticles.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", delay);
    }
    void NextLevel(){
        
        int activeLevel = SceneManager.GetActiveScene().buildIndex;
        int nextLevel = activeLevel + 1;

        if(nextLevel == SceneManager.sceneCountInBuildSettings){
            nextLevel = 0;
        }

        SceneManager.LoadScene(nextLevel);
    }

    void LoadingNextLevel(){
        isControllable = false;
        audioSource.Stop();
        audioSource.PlayOneShot(VictorySFX);
        VictoryParticles.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("NextLevel", delay);
    }

    void RespondToDebugKeys(){
        if(Keyboard.current.lKey.wasPressedThisFrame){
            NextLevel();
        }
        else if(Keyboard.current.cKey.wasPressedThisFrame){
            isCollidable = !isCollidable;
        }
    }


}
