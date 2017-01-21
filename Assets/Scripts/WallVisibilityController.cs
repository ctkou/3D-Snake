using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallVisibilityController : MonoBehaviour {
    
    public Camera mainCamera;

    private Material meshMaterial;

    private float TRANSPARENT_ALPHA = 0.5f;
    private float OPAQUE_ALPHA = 1.0f;
    private bool isInvisible = false;

    void Start()
    {
        meshMaterial = this.GetComponent<MeshRenderer>().material;
    }

    // Update is called once per frame
    void Update ()
    {
		if (isCameraOutsideWall() && isInvisible == false)
        {
            isInvisible = true;
            meshMaterial.color = new Color(
                meshMaterial.color.r,
                meshMaterial.color.g,
                meshMaterial.color.b,
                TRANSPARENT_ALPHA
            );
        }
        else if (!isCameraOutsideWall() && isInvisible)
        {
            isInvisible = false;
            meshMaterial.color = new Color(
                meshMaterial.color.r,
                meshMaterial.color.g,
                meshMaterial.color.b,
                OPAQUE_ALPHA
            );
        }
	}

    private bool isCameraOutsideWall()
    {
        return 
            (Mathf.Abs(mainCamera.transform.position.x) > Mathf.Abs(transform.position.x) && Mathf.Abs(transform.position.x) > 1.0f) ||
            (Mathf.Abs(mainCamera.transform.position.y) > Mathf.Abs(transform.position.y) && Mathf.Abs(transform.position.y) > 1.0f) ||
            (Mathf.Abs(mainCamera.transform.position.z) > Mathf.Abs(transform.position.z) && Mathf.Abs(transform.position.z) > 1.0f);
    }
}
