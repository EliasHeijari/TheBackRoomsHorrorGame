using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWalls : MonoBehaviour
{
    [SerializeField] private Material effectMat;
    private MeshRenderer meshRenderer;
    private Material oldMat;
    bool hasMaterialSet = false;

    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        oldMat = meshRenderer.materials[1];
        Hallucination.OnHallucinationWalls += Hallucination_OnHallucinationWalls;
        Hallucination.OnHallucinationWallsOff += Hallucination_OnHallucinationWallsOff;
    }

    private void Hallucination_OnHallucinationWalls(object sender, System.EventArgs e)
    {
        if (!hasMaterialSet)
        {
            AddMoveFloorShader();
            hasMaterialSet = true;
        }
    }

    private void Hallucination_OnHallucinationWallsOff(object sender, System.EventArgs e)
    {
        if (hasMaterialSet)
        {
            ResetMaterial();
            hasMaterialSet = false;
        }
    }

    public void AddMoveFloorShader()
    {
        Material[] mats = meshRenderer.materials;
        mats[1] = effectMat;
        meshRenderer.materials = mats;
    }
    public void ResetMaterial()
    {
        Material[] mats = meshRenderer.materials;
        mats[1] = oldMat;
        meshRenderer.materials = mats;
    }
}
