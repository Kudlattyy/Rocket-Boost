using UnityEngine;
using UnityEngine.SceneManagement;
public class CollisionHandler : MonoBehaviour
{
    private void OnCollisionEnter(Collision other) {
        switch(other.gameObject.tag){
            case "Friendly":
                Debug.Log("Jesteś na lini startu");
                break;
            case "Finish":
                nextLevel();
                break;
            case "Fuel":
                Debug.Log("Zebrałeś paliwko");
                break;
            default:
                reloadLevel();
                break;
        }
    }

    void reloadLevel(){
        int activeLevel = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(activeLevel);
    }

    void nextLevel(){
        int activeLevel = SceneManager.GetActiveScene().buildIndex;
        int nextLevel = activeLevel + 1;

        if(nextLevel == SceneManager.sceneCountInBuildSettings){
            nextLevel = 0;
        }

        SceneManager.LoadScene(nextLevel);
    }

}
