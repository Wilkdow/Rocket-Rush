using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    void OnCollisionEnter(Collision other)
    {
        switch(other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("You bumped into a friendly object");
                break;

            case "Finish":
                Debug.Log("You finished the level!");
                break;

            case "Fuel":
                Debug.Log("You catched some fuel!");
                break;

            default:
                Debug.Log("You crashed!");
                ReloadScene();
                break;
        }
    }

    void ReloadScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
