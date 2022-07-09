using System;
using UnityEngine;

public class KeyboardKey : MonoBehaviour
{
  
    [SerializeField]
    private TMPro.TextMeshPro textMeshPro;

    [SerializeField]
    private MeshRenderer meshRenderer;

    public string keyValue {
         get; private set;
    }

    public void updateKeyValue(string value) {
        keyValue = value;
        textMeshPro.SetText(keyValue);
    }

    public void updateKeyColor(Color colour) {
        meshRenderer.material.color = colour;
    }

}
