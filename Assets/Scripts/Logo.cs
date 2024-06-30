using UnityEngine;
using TMPro;

public class ColorRandomizer : MonoBehaviour
{
    public TextMeshProUGUI textMeshPro;
    public float changeInterval = 2f;
    public float transitionDuration = 1f;

    private Color targetColor;
    private Color initialColor;
    private float transitionStartTime;

    void Start()
    {
        initialColor = textMeshPro.color;
        InvokeRepeating("ChangeColor", changeInterval, changeInterval);
    }

    void ChangeColor()
    {
        float r = Random.value;
        float g = Random.value;
        float b = Random.value;

        targetColor = new Color(r, g, b);
        transitionStartTime = Time.time;
    }

    void Update()
    {
        if (Time.time - transitionStartTime < transitionDuration)
        {
            float t = (Time.time - transitionStartTime) / transitionDuration;
            Color lerpedColor = Color.Lerp(initialColor, targetColor, t);
            textMeshPro.color = lerpedColor;
        }
        else
        {
            textMeshPro.color = targetColor;
            initialColor = targetColor;
        }
    }
}
