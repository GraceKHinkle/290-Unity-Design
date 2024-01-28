using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class RayShooter : MonoBehaviour {
    private Camera cam;
    private RaycastHit hit;
    private float centerX; // Declare centerX at the class level
    private float centerY; // Declare centerY at the class level
    void Start() {
        cam = GetComponent<Camera>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    void OnGUI() {
        int size = 50;
        centerX = cam.pixelWidth / 2 - size / 4; // Assign the value to centerX
        centerY = cam.pixelHeight / 2 - size / 2; // Assign the value to centerY
        GUI.Label(new Rect(centerX, centerY, size, size), "+");

        //Display coordinates of the raycast hit
        if (Event.current.type == EventType.Repaint)
        {
            float labelWidth = 2500;
            float labelHeight = 500;
            float labelX = 10; // Adjusted to the left corner
            float labelY = 10; // Adjusted to the top corner

            GUI.Label(new Rect(labelX, labelY, labelWidth, labelHeight), "Hit Point: " + hit.point.ToString());
        }
    }
    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            Vector3 point = new Vector3(cam.pixelWidth/2, cam.pixelHeight/2, 0);
            Ray ray = cam.ScreenPointToRay(point);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit)) {
                GameObject hitObject = hit.transform.gameObject;
                ReactiveTarget target = hitObject.GetComponent<ReactiveTarget>();
                    if (target != null) {
                        target.ReactToHit();
                    } else {
                        StartCoroutine(SphereIndicator(hit.point));
                    }
            }
        }
    }
    private IEnumerator SphereIndicator(Vector3 pos) {
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.position = pos;
        yield return new WaitForSeconds(1);
        Destroy(sphere);
        }
}