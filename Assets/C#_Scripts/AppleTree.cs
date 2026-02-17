using UnityEngine;

public class AppleTree : MonoBehaviour
{
    [Header("Inscribed")]
    public GameObject applePrefab;

    // 5: Branch support
    public GameObject branchPrefab;
    [Range(0f, 1f)]
    public float branchDropChance = 0.07f; // branches much less frequent than apples

    public float speed = 1f;
    public float leftAndRightEdge = 10f;
    public float changeDirChance = 0.1f;
    public float appleDropDelay = 1f;

    void Start()
    {
        Invoke(nameof(DropApple), 2f);
    }

    void DropApple()
    {
        GameObject objToDrop = applePrefab;

        if (branchPrefab != null && Random.value < branchDropChance)
        {
            objToDrop = branchPrefab;
        }

        GameObject dropped = Instantiate<GameObject>(objToDrop);
        dropped.transform.position = transform.position;

        Invoke(nameof(DropApple), appleDropDelay);
    }

    void Update()
    {
        Vector3 pos = transform.position;
        pos.x += speed * Time.deltaTime;
        transform.position = pos;

        if (Random.value < changeDirChance)
        {
            speed *= -1;
        }

        if (pos.x < -leftAndRightEdge) speed = Mathf.Abs(speed);
        else if (pos.x > leftAndRightEdge) speed = -Mathf.Abs(speed);
    }
}
