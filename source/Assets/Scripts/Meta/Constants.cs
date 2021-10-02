using UnityEngine;

public static class Constants
{
    public static readonly Camera MainCamera;
    public static readonly float CameraAngularRotation;
    public static readonly float CameraSize;

    static Constants()
    {
        MainCamera = Camera.main;
        CameraAngularRotation = Mathf.Deg2Rad * MainCamera.transform.rotation.eulerAngles.x;
        CameraSize = MainCamera.orthographicSize * 2;
    }

    public static float GameFieldSizeX =>
        Vector2.Distance(MainCamera.ScreenToWorldPoint(new Vector2(0, 0)),
            MainCamera.ScreenToWorldPoint(new Vector2(Screen.width, 0)));

    public static float GameFieldSizeZ =>
        CameraSize / Mathf.Sin(CameraAngularRotation);

    public static float GetGameFieldLowerBoundZ(float height)
    {
        return (height - Mathf.Cos(CameraAngularRotation) * CameraSize * .5f) /
               Mathf.Tan(CameraAngularRotation) -
               (Mathf.Sin(CameraAngularRotation) * CameraSize * .5f) + MainCamera.transform.position.z;
    }

    public static float GetGameFieldUpperBoundZ(float height)
    {
        return GetGameFieldLowerBoundZ(height) + GameFieldSizeZ;
    }
}