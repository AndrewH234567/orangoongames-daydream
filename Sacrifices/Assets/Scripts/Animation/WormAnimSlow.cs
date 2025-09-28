using UnityEngine;

public class WormAnimSLow : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        // Find the GameObject named "Player" in the scene
        GameObject player = GameObject.Find("Enemy");

        if (player != null)
        {
            animator = player.GetComponent<Animator>();
            animator.speed = 0.3f; // slow it down here
        }
        else
        {
            Debug.LogError("Player GameObject not found!");
        }
    }
}