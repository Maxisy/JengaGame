using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public bool IsSpecial = false;
    public bool Enabled = true;

    public Material NormalMaterial;
    public Material SpecialMaterial;
    void Start()
    {
        SetMaterial();
    }

    private void OnValidate()
    {
        SetMaterial();
        Enabled = true;
    }

    void Update()
    {
        
    }

    void SetMaterial()
    {
        var targetMaterial = IsSpecial ? SpecialMaterial : NormalMaterial;

        var renderer = GetComponent<Renderer>();
        renderer.material = targetMaterial;
    }

    private void OnMouseDrag()
    {
        var cameraPosition = FindObjectOfType<Camera>().transform.position;
        var direction = (cameraPosition - transform.position).normalized;

        GetComponent<Rigidbody>().AddForce(direction * 15);
    }
}
