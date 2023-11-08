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
        Hallucination.OnHallucinationMedium += Hallucination_OnHallucinationMedium;
        Hallucination.OnHallucinationMediumOff += Hallucination_OnHallucinationMediumOff;
    }

    private void Hallucination_OnHallucinationMedium(object sender, System.EventArgs e)
    {
        if (!hasMaterialSet)
        {
            AddMoveFloorShader();
            hasMaterialSet = true;
        }
    }

    private void Hallucination_OnHallucinationMediumOff(object sender, System.EventArgs e)
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
