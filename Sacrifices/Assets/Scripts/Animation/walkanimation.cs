using UnityEngine;

public class SlowDownAnimation : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        // Find the GameObject named "Player" in the scene
        GameObject player = GameObject.Find("Player");

        if (player != null)
        {
            animator = player.GetComponent<Animator>();
            animator.speed = 0.5f; // slow it down here
        }
        else
        {
            Debug.LogError("Player GameObject not found!");
        }
    }
}