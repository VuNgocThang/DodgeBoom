using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lam
{
    public class ScreenEdgeColliders : MonoBehaviour
    {
        private Camera cam;
        private EdgeCollider2D edge;
        private Vector2[] edgePoints;

        IEnumerator Start()
        {
            yield return new WaitForEndOfFrame();
            if (Camera.main == null) Debug.LogError("Camera.main not found, failed to create edge colliders");
            else cam = Camera.main;

            if (!cam.orthographic) Debug.LogError("Camera.main is not Orthographic, failed to create edge colliders");

            // add or use existing EdgeCollider2D
            edge = GetComponent<EdgeCollider2D>() == null ? gameObject.AddComponent<EdgeCollider2D>() : GetComponent<EdgeCollider2D>();

            edgePoints = new Vector2[5];

            AddCollider();
        }

        //Use this if you're okay with using the global fields and code in Awake() (more efficient)
        //You can just ignore/delete StandaloneAddCollider() if thats the case
        void AddCollider()
        {
            //Vector2's for the corners of the screen
            Vector2 bottomLeft = cam.ScreenToWorldPoint(new Vector3(0, 0, cam.nearClipPlane));
            Vector2 topRight = cam.ScreenToWorldPoint(new Vector3(cam.pixelWidth, cam.pixelHeight, cam.nearClipPlane));
            Vector2 topLeft = new Vector2(bottomLeft.x, topRight.y);
            Vector2 bottomRight = new Vector2(topRight.x, bottomLeft.y);

            //Update Vector2 array for edge collider
            edgePoints[0] = bottomLeft;
            edgePoints[1] = topLeft;
            edgePoints[2] = topRight;
            edgePoints[3] = bottomRight;
            edgePoints[4] = bottomLeft;

            edge.points = edgePoints;
        }

        //Use this if you want a single function to handle everything (less efficient)
        //You can just ignore/delete the rest of this class if thats the case
        void StandaloneAddCollider()
        {
            if (Camera.main == null) { Debug.LogError("Camera.main not found, failed to create edge colliders"); return; }

            var cam = Camera.main;
            if (!cam.orthographic) { Debug.LogError("Camera.main is not Orthographic, failed to create edge colliders"); return; }

            Vector2 bottomLeft = cam.ScreenToWorldPoint(new Vector3(0, 0, cam.nearClipPlane));
            Vector2 topRight = cam.ScreenToWorldPoint(new Vector3(cam.pixelWidth, cam.pixelHeight, cam.nearClipPlane));
            Vector2 topLeft = new Vector2(bottomLeft.x, topRight.y);
            Vector2 bottomRight = new Vector2(topRight.x, bottomLeft.y);

            // add or use existing EdgeCollider2D
            var edge = GetComponent<EdgeCollider2D>() == null ? gameObject.AddComponent<EdgeCollider2D>() : GetComponent<EdgeCollider2D>();

            var edgePoints = new[] { bottomLeft, topLeft, topRight, bottomRight, bottomLeft };
            edge.points = edgePoints;
        }
    }
}


