using UnityEngine;

public class BoundsCheck : MonoBehaviour
{
    public enum eType {center, inset, outset};

    [Header("Inscribed")]
    public eType boundsType = eType.center;
    public float radius = 1f;
    public bool keepOnScreen = true;

    [Header("Dynamic")]
    public float camWidth;
    public float camHeight;
    public bool isOnScreen = true;
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
        isOnScreen = true;

        // restrict the X position to camWidth
        if (pos.x > camWidth + checkRadius){
            pos.x = camWidth + checkRadius;
            isOnScreen = false;
        }
        if (pos.x < -camWidth - checkRadius){
            pos.x = -camWidth - checkRadius;
            isOnScreen = false;
        }
        // Restrict the Y position to cmaHeight
        if (pos.y > camHeight + checkRadius){
            pos.y = camHeight + checkRadius;
            isOnScreen = false;
        }
        if (pos.y < -camHeight - checkRadius){
            pos.y = -camHeight - checkRadius;
            isOnScreen = false;
        }

        if (keepOnScreen && !isOnScreen){
            transform.position = pos;
            isOnScreen = true;
        }
    }

}
