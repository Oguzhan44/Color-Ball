using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fountain : MonoBehaviour
{
    public int colorNo = 1;
    [SerializeField] ParticleSystem particle;
    [SerializeField] List<MeshRenderer> pipeMeshes;
    private void Start()
    {
        ParticleSystem.MainModule mm = particle.main;
        Color fountainColor = (Color)GameManager.Instance.GetColor(colorNo);
        mm.startColor = fountainColor;

        Material newMat = new Material(Shader.Find("Standard"));
        newMat.SetColor("_Color", fountainColor);
        newMat.SetFloat("_Metallic", 0.5F);
        newMat.SetFloat("_Glossiness", 0.5F);

        foreach(var item in pipeMeshes)
        {
            item.material = newMat;
        }
    }
}
