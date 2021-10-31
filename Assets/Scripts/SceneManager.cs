using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour {

    protected void Awake() {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Battle", LoadSceneMode.Additive);
    }

}
