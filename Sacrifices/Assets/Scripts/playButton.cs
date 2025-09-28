using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public void LoadMain()
    {
        SceneManager.LoadScene("Main");
    }
}
