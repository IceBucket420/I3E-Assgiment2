/*
 * Author: Pang Le Xin (Reference from Brackeys YT video)
 * Date: 29/06/2023
 * Description: Healthbar script that lets the the health bar decrease in value in player canvas
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    /// <summary>
    /// Slider for healthbar
    /// </summary>
    public Slider HealthSlider;
    /// <summary>
    /// Gradient for healthbar
    /// </summary>
    public Gradient gradient;
    /// <summary>
    /// image of the fill
    /// </summary>
    public Image fill;

    /// <summary>
    /// function set maximum of health of the health bar of player
    /// </summary>
    /// <param name="health"></param>
    public void SetMaxHealth(int health)
    {
        HealthSlider.maxValue = health;
        HealthSlider.value = health;

        fill.color = gradient.Evaluate(1f);

    }

    /// <summary>
    /// function that set the health of the player in the health bar
    /// </summary>
    /// <param name="health"></param>
    public void SetHealth(int health)
    {
        HealthSlider.value = health;

        fill.color = gradient.Evaluate(HealthSlider.normalizedValue);

    }
}
