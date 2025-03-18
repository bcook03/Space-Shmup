using UnityEngine;

public class BoundsCheck : MonoBehaviour
{
    [Header("Dynamic")]
    public float camWidth;
    public float camHeight;
    /// <summary>
    /// Keeps a GameObject on screen.
    /// Note that this ONLY works for an orhographic Main Camera
    /// </summary>
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake() {
        camHeight = Camera.main.orthographicSize;
        camWidth = camHeight * Camera.main.aspect;
    }

    void LateUpdate() {
        Vector3 pos = transform.position;

        // restrict the X position to camWidth
        if (pos.x > camWidth)
            pos.x = camWidth;
        if (pos.x < -camWidth)
            pos.x = -camWidth;
        
        // Restrict the Y position to cmaHeight
        if (pos.y > camHeight)
            pos.y = camHeight;
        if (pos.y < -camHeight)
            pos.y = -camHeight;

        transform.position = pos;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
