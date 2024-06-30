using UnityEngine;

public class RotateSkybox : MonoBehaviour
{
    public float rotationSpeed = 10f;
    private bool reverseRotation = false;

    void Update()
    {
      float newRotation = Time.time * rotationSpeed % 360f;
     if (newRotation >= 360f)
            reverseRotation = !reverseRotation;

         if (reverseRotation)
            newRotation = 360f - newRotation;
 RenderSettings.skybox.SetFloat("_Rotation", newRotation);
    }
}
