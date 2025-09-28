using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public Entity entity;

    public void Awake()
    {
        slider = GetComponent<Slider>();
    }

    public void Update()
    {
        slider.value = entity.GetHp();
    }
}