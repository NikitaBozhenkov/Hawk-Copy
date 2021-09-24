using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundaries : MonoBehaviour
{
    public Camera MainCamera;
    private Vector2 screenBounds;
    private float objectWidth;
    private float objectHeight;
    private Vector2 screenSize;

    private float lowBnd;
    private float upBnd;

    void Start() {
        objectWidth = transform.GetComponent<MeshRenderer>().bounds.extents.x;
        objectHeight = transform.GetComponent<MeshRenderer>().bounds.extents.z;
        screenSize.x = Vector2.Distance(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)), Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0))) * 0.5f;

        screenBounds = MainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, MainCamera.transform.position.z));
    }

    void FixedUpdate() {
        var rot = Mathf.Deg2Rad * MainCamera.transform.rotation.eulerAngles.x;
        var h = MainCamera.transform.position.y - transform.position.y;
        var size = MainCamera.orthographicSize * 2;
        lowBnd = (h - Mathf.Cos(rot) * size / 2f) / Mathf.Tan(rot) - (Mathf.Sin(rot) * size / 2f);
        lowBnd += MainCamera.transform.position.z;
        upBnd = lowBnd + size / Mathf.Sin(rot);
        Vector3 viewPos = transform.position;

        viewPos.x = Mathf.Clamp(viewPos.x, screenBounds.x - screenSize.x * 2 + objectWidth, screenBounds.x - objectWidth);
        viewPos.z = Mathf.Clamp(viewPos.z, lowBnd + objectWidth, upBnd - objectHeight);
        transform.position = viewPos;
    }
}
