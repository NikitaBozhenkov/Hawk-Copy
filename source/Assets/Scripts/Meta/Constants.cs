using UnityEngine;

public static class Constants
{ 
    public static Rect GetCameraProjectionOnPlaneXZ(Camera camera, float height)
    {
        float cameraAngularRotation = Mathf.Deg2Rad * camera.transform.rotation.eulerAngles.x;
        float cameraSize = camera.orthographicSize * 2;
        float sizeX = Vector2.Distance(camera.ScreenToWorldPoint(new Vector2(0, 0)),
            camera.ScreenToWorldPoint(new Vector2(Screen.width, 0)));
        float sizeZ = cameraSize / Mathf.Sin(cameraAngularRotation);
        float zMin = (height - Mathf.Cos(cameraAngularRotation) * cameraSize * .5f) /
            Mathf.Tan(cameraAngularRotation) -
            (Mathf.Sin(cameraAngularRotation) * cameraSize * .5f) + camera.transform.position.z;
        Debug.Log(zMin + " " + sizeZ);
            
        Rect rect = new Rect(-sizeX / 2, zMin, sizeX, sizeZ);
        return rect;
    } 
}