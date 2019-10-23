using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meshdeformation : MonoBehaviour
{
    private Mesh deformingMesh;
    Vector3[] originalVertices;
    private Vector3[] displacedVertices;
    // Start is called before the first frame update
    void Start()
    {
        deformingMesh = GetComponent<MeshFilter>().mesh;
        originalVertices = deformingMesh.vertices;
        displacedVertices = new Vector3[originalVertices.Length];
        int[] indices = deformingMesh.triangles;
        for (var i = 0; i < originalVertices.Length; i++)
        {
            Vector3 vectornormal;
            vectornormal = transform.TransformDirection(deformingMesh.normals[i]);
            if (vectornormal.y > 0)
            {
                displacedVertices[i] = -Vector3.up * Random.Range(1.0f,3.0f);
            }
            else
            {
                displacedVertices[i] = originalVertices[i];
            }
        }
        StartCoroutine(ChangeShape(originalVertices, displacedVertices,Random.Range(0.0f,2.0f)));
    }

    public IEnumerator ChangeShape(Vector3[] start, Vector3[] end, float time)
    {
        float t = 0f;
        float at = 1.0f / time ;
        Vector3[] FreshArray = deformingMesh.vertices;
        while (t < at)
        {
            for (int i = 0; i < FreshArray.Length;i++)
            {
                Debug.Log("A " + t);
                FreshArray[i] = Vector3.Lerp(start[i], end[i], t);
            }

            t += Time.deltaTime;
            deformingMesh.vertices = FreshArray;
        }
        StartCoroutine(WaitAndRevert());
        //deformingMesh.RecalculateBounds();
        yield return null;
    }

    public IEnumerator WaitAndRevert()
    {
        yield return new WaitForSeconds(Random.Range(0.0f, 3.0f));
        StartCoroutine(ChangeShape(displacedVertices, originalVertices, Random.Range(0.0f, 3.0f)));
        yield return null;
    }
    
}
