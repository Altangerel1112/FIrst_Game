using UnityEngine;

public class Branch : MonoBehaviour
{
    public static float bottomY = -20f;

    void Update()
    {
        if (transform.position.y < bottomY)
        {
            Destroy(gameObject);
        }
    }
}
