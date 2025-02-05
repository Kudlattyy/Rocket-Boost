using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    private void OnCollisionEnter(Collision other) {
        switch(other.gameObject.tag){
            case "Friendly":
                Debug.Log("Jesteś na lini startu");
                break;
            case "Finish":
                Debug.Log("Brawo ukończyłeś level");
                break;
            case "Fuel":
                Debug.Log("Zebrałeś paliwko");
                break;
            default:
                Debug.Log("Rozbiłeś się");
                break;
        }
    }
}
