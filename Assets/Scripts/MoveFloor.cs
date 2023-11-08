using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveFloor : MonoBehaviour
{
    [SerializeField] private Material effectMat;
    private MeshRenderer meshRenderer;
    private Material oldMat;

    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        oldMat = meshRenderer.material;
    }

    public void AddMoveFloorShader()
    {
        meshRenderer.material = effectMat;
    }
    public void ResetMaterial()
    {
        meshRenderer.material = oldMat;
    }
}
