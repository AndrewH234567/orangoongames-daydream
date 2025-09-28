using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class Alert : MonoBehaviour
{
    public TMP_Text alert;  // Drag your TMP text here in Inspector

    public static Alert Instance;

    void Start()
    {
        Instance = this;
        alert.text = "";
    }

    public void Update(string message)
    {
        alert.text = message;
    }
}
