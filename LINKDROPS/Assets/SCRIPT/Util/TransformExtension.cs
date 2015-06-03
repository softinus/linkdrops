using UnityEngine;
using System.Collections;

public static class TransformExtensions
{
    /// <summary> 
    /// 지정된 객체의 방향으로 회전 
    /// </ summary> 
    /// <param name = "self"> Self. </ param> 
    /// <param name = "target"> Target. </ param> 
    /// <param name = "forward"> 앞쪽 </ param> 
    public static void LookAt2D(this  Transform self, Transform target, Vector2 forward)
    {
        LookAt2D(self, target.position, forward);
    }

    public static void LookAt2D(this  Transform self, Vector3 target, Vector2 forward)
    {
        var forwardDiff = GetForwardDiffPoint(forward);
        Vector3 direction = target - self.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        self.rotation = Quaternion.AngleAxis(angle - forwardDiff, Vector3.forward);
    }

    /// <summary> 
    /// 정면 방향 차이를 계산 
    /// </ summary> 
    /// <returns> The forward diff point. </ returns> 
    /// <param name = "forward"> Forward </ param> 
    static private float GetForwardDiffPoint(Vector2 forward)
    {
        if (Equals(forward, Vector2.up)) return 90;
        if (Equals(forward, Vector2.right)) return 0;
        return 0;
    }

    // Ortographic camera zoom towards a point (in world coordinates). Negative amount zooms in, positive zooms out
    // TODO: when reaching zoom limits, stop camera movement as well
    public static void ZoomOrthoCamera(Vector3 zoomTowards, float amount, Camera camera, float fMin, float fMax)
    {
        // Calculate how much we will have to move towards the zoomTowards position
        float multiplier = (1.0f / camera.orthographicSize * amount);

        // Move camera
        camera.transform.position += (zoomTowards - camera.transform.position) * multiplier;

        // Zoom camera
        camera.orthographicSize -= amount;

        // Limit zoom
        camera.orthographicSize = Mathf.Clamp(camera.orthographicSize, fMin, fMax);
    }
}