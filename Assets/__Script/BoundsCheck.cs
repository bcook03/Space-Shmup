using UnityEngine;

public class BoundsCheck : MonoBehaviour
{
    public enum eType {center, inset, outset};

    [Header("Inscribed")]
    public eType boundsType = eType.center;
    public floar radius = 1f;

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
        // Find the checkRadius that will enable center, inset, or outset
        float checkRadius = 0;
        if (boundsType == eType.inset) checkRadius = -radius;
        if (boundsType == eType.outset) checkRadius = radius;

        Vector3 pos = transform.position;

        // restrict the X position to camWidth
        if (pos.x > camWidth + checkRadius)
            pos.x = camWidth + checkRadius;
        if (pos.x < -camWidth - checkRadius)
            pos.x = -camWidth - checkRadius;
        
        // Restrict the Y position to cmaHeight
        if (pos.y > camHeight + checkRadius)
            pos.y = camHeight + checkRadius;
        if (pos.y < -camHeight - checkRadius)
            pos.y = -camHeight - checkRadius;

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
