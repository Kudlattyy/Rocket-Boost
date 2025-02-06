using UnityEngine;
using UnityEngine.SceneManagement;
public class CollisionHandler : MonoBehaviour
{

    [SerializeField] float delay = 2f;

    [SerializeField] AudioClip Victory;
    [SerializeField] AudioClip Defeat;
    
    AudioSource audioSource;

    bool isControllable = true;
 
    void Start(){
        audioSource = GetComponent<AudioSource>();
    }

    
    private void OnCollisionEnter(Collision other) {
        if(!isControllable) {return;}
        switch(other.gameObject.tag){
            case "Friendly":
                Debug.Log("Jesteś na lini startu");
                break;
            case "Finish":
                LoadingNextLevel();
                break;
            case "Fuel":
                Debug.Log("Zebrałeś paliwko");
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
        audioSource.PlayOneShot(Defeat);
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
        audioSource.PlayOneShot(Victory);
        GetComponent<Movement>().enabled = false;
        Invoke("NextLevel", delay);
    }

}
