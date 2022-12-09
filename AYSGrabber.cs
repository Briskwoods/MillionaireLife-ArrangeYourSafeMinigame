using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AYSGrabber : MonoBehaviour
{

    // Raycast Control Variable
    [SerializeField] private Camera m_mainCamera;

    [SerializeField] private int m_handSpeed = 75;

    public GameObject selectedObject;

    public bool m_isDragging;

    private void Update()
    {

        #region PC Controls
        //PC Controls Test Code, uncoomment if you wish to Test with a mouse instead of Unity Remote 5

        switch (Input.GetMouseButtonDown(0))
        {
            case true:
                m_isDragging = true;
                break;
            case false:
                break;
        }

        switch (Input.GetMouseButtonUp(0))
        {
            case true:
                m_isDragging = false;
                break;
            case false:
                break;
        }
        #endregion



        switch (m_isDragging)
        {
            case true:
                switch (selectedObject == null)
                {
                    case true:
                        RaycastHit hit = CastRay();

                        switch (hit.collider != null)
                        {
                            case true:
                                switch (!hit.collider.CompareTag("AYSObjects"))
                                {
                                    case true: return;
                                    case false: break;
                                }
                                selectedObject = hit.collider.gameObject;
                                selectedObject.GetComponent<AYSObjectSelectionController>().isSelected = true;
                                Cursor.visible = false;
                                break;
                            case false:
                                break;

                        }
                        break;
                    case false:

                        break;
                }
                break;
            case false:

                switch (selectedObject != null)
                {
                    case true:
                        selectedObject.gameObject.GetComponent<AYSReturnToOrigin>().BackToOrigin();
                        selectedObject.GetComponent<AYSObjectSelectionController>().isSelected = false;
                        Vector3 position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.WorldToScreenPoint(selectedObject.transform.position).z);
                        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(position);
                        selectedObject.transform.position = new Vector3(worldPosition.x, worldPosition.y, worldPosition.z);

                        selectedObject = null;
                        Cursor.visible = true;
                        break;
                    case false: break;
                }
                break;
        }

        switch (selectedObject != null)
        {
            case true:
                Vector3 position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.WorldToScreenPoint(selectedObject.transform.position).z);
                Vector3 worldPosition = Camera.main.ScreenToWorldPoint(position);
                selectedObject.transform.position = new Vector3(worldPosition.x, worldPosition.y, worldPosition.z);
                break;
            case false: break;
        }
    }

    private RaycastHit CastRay()
    {
        Vector3 screenMousePosFar = new Vector3(
            Input.mousePosition.x,
            Input.mousePosition.y,
            Camera.main.farClipPlane);
        Vector3 screenMousePosNear = new Vector3(
            Input.mousePosition.x,
            Input.mousePosition.y,
            Camera.main.nearClipPlane);
        Vector3 worldMousePosFar = Camera.main.ScreenToWorldPoint(screenMousePosFar);
        Vector3 worldMousePosNear = Camera.main.ScreenToWorldPoint(screenMousePosNear);
        RaycastHit hit;
        Physics.Raycast(worldMousePosNear, worldMousePosFar - worldMousePosNear, out hit);

        return hit;
    }
}
