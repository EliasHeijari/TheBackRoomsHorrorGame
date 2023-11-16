using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManualCameraRendering : MonoBehaviour
{
    [SerializeField] private int fps = 8;
    float elapsed;
    Camera cam;
    [SerializeField] Transform playerTransform;
    [SerializeField] Transform SecurityTVTransform;

    private bool IsFacingObject()
    {
        Vector3 toPlayer = (playerTransform.position - SecurityTVTransform.position).normalized;
        Vector3 forward = playerTransform.forward;
        float dot = Vector3.Dot(toPlayer, forward);

        if (dot < 0f)
        {
            if (Vector3.Distance(playerTransform.position, SecurityTVTransform.position) < 19f)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    private void Start()
    {
        cam = GetComponent<Camera>();
        cam.enabled = false;
    }

    private void Update()
    {
        if (gameObject.activeSelf == true && IsFacingObject() == true) 
        {
            elapsed += Time.unscaledDeltaTime;
            if (elapsed > 1f / fps)
            {
                elapsed = 0;
                cam.Render();
            }
            else if (gameObject.activeSelf == false && cam.enabled == true)
            {
                cam.enabled = false;
            }
        }
    }


}
