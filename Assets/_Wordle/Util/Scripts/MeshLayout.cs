using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshLayout : MonoBehaviour, Layout
{
    public float calculateHeight() {
        //find first meshfilter
        Transform transformWithMesh = findFirstGameObjectWithMeshFilter(this.transform);
         if (transformWithMesh != null) {
            MeshFilter mesh = transformWithMesh.GetComponent<MeshFilter>();
            return mesh.mesh.bounds.size.y * transformWithMesh.transform.localScale.y;
        } 
        return 0f;
    }

    public Transform findFirstGameObjectWithMeshFilter(Transform gameObject) {
        MeshFilter mesh = gameObject.GetComponent<MeshFilter>();
        if (mesh == null) {
            for (int i = 0; i < gameObject.transform.childCount; i++) {
                Transform childGameObject = findFirstGameObjectWithMeshFilter(gameObject.transform.GetChild(i));
                if (childGameObject != null) {
                    return childGameObject;
                }
            }             
        }
        return gameObject;
    }

    public float calculateWidth() {
        //find first meshfilter
        Transform transformWithMesh = findFirstGameObjectWithMeshFilter(transform);
        if (transformWithMesh != null) {
            MeshFilter mesh = transformWithMesh.GetComponent<MeshFilter>();
            return mesh.mesh.bounds.size.x * transformWithMesh.transform.localScale.x;
        }
        return 0f;
    }
}
