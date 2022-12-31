using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
#if UNITY_EDITOR
using UnityEditor;
#endif

[System.Serializable]
#if UNITY_EDITOR
[CanEditMultipleObjects]
#endif
public class Scanner
{
    public LayerMask layerMaskTarget, Obstacle, layerMaskSubTarget;
    public Material materialFieldOfView;
    private Mesh mesh;
    private MeshFilter meshFilterFOV;
    private float fov, ViewDistence;
    private Transform _detector;
    private List<RaycastHit> listHit = new List<RaycastHit>();
    public UnityEvent<List<RaycastHit>> OnDetectedTarget;
    public UnityEvent<Transform> OnDetectedSubTarget;
    public UnityEvent OnNotDetectedTarget;

    public GameObject CreataFieldOfView(Transform detector, Vector3 pos, float angel, float distance) {
        //creata field of view
        _detector = detector;
        mesh = new Mesh();
        GameObject FieldOfView = new GameObject("FieldOfView");
        FieldOfView.transform.SetParent(_detector);
        FieldOfView.transform.position = pos;
        FieldOfView.transform.rotation = _detector.rotation;
        MeshRenderer meshRendererFOV = FieldOfView.AddComponent<MeshRenderer>();
        meshFilterFOV =  FieldOfView.AddComponent<MeshFilter>();
        meshRendererFOV.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        meshRendererFOV.material = materialFieldOfView;
        meshFilterFOV.mesh = mesh;
        fov = angel;
        ViewDistence = distance;
        return FieldOfView;
    }


    public void Scan() {
        // remove all element in list detect
        listHit.RemoveAll(el => true);

        int rayCount = 25;
        float angelIncrease = fov/rayCount;

        //init vetex, uv, triangle
        Vector3[] vertices = new Vector3[rayCount + 2];
        Vector2[] uv = new Vector2[vertices.Length];
        int[] triangles = new int[rayCount * 3];

        float angel = -fov/2;
        vertices[0] = Vector3.zero;
        int triangleIndex = 0;

        //set vetex and triangle for mesh
        for(int i = 0; i<=rayCount; i++) {
            // render FOV when have Obstacle
            Vector3 dir = Quaternion.Euler(0, angel, 0) * Vector3.forward;
            Vector3 dirRaycast = Quaternion.Euler(0, angel, 0) * _detector.forward.normalized;

            Ray origin = new Ray(_detector.position, dirRaycast);
            Vector3 vertex = Vector3.zero +  (dir * ViewDistence);
            float rangeScan = ViewDistence;
            RaycastHit hit;

            if(Physics.Raycast(origin, out hit, ViewDistence, Obstacle)) {
                vertex = Vector3.zero +  (dir * hit.distance);
                rangeScan = hit.distance;
            }

            // set triangels for custom mesh
            vertices[i + 1] = vertex;
            if(i>0) {
                triangles[triangleIndex] = 0;
                triangles[triangleIndex + 1] = i;
                triangles[triangleIndex + 2] = i + 1;
                triangleIndex += 3;
            }

            angel += angelIncrease;
            
            // scan object by raycast
            ScanTarget(origin, rangeScan);

            // scan sub object ex: dead body
            bool detectSubTarget =  false;
            ScanSubTarget(origin, rangeScan, ref detectSubTarget);
        }

        if (listHit.Count > 0)
        {
            OnDetectedTarget?.Invoke(listHit);
        }
        else
        {
            OnNotDetectedTarget?.Invoke();
        }

        
        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;
    }

    private void ScanTarget(Ray origin, float range) {
        RaycastHit hit;
        Vector3 end = origin.GetPoint(range - 0.5f);
        if(Physics.Linecast(origin.origin, end, out hit, layerMaskTarget)) {
            listHit.Add(hit);
        }
    }

    private void ScanSubTarget(Ray origin, float range, ref bool detected) {
        if(!detected) {
            RaycastHit hit;
            Vector3 end = origin.GetPoint(range - 0.5f);
            if(Physics.Linecast(origin.origin, end, out hit, layerMaskSubTarget)) {
                OnDetectedSubTarget?.Invoke(hit.transform);
                detected = true;
            }
        }
    }

    public Transform DetectSingleTarget(List<RaycastHit> listRaycast) {
        float[] distences = new float[listRaycast.Count];
        for(int i = 0; i<listRaycast.Count; i++) {
            distences[i] = listRaycast[i].distance;
        }

        float minDistence = Mathf.Min(distences);
        int hitIndex = Array.IndexOf(distences, minDistence);
        return listRaycast[hitIndex].transform;
    }

#if UNITY_EDITOR
    public void EditorGizmo(Transform transform, float angle, float radius) {
        Handles.color = new Color32(76, 122, 90, 80);
        Vector3 vectorStart = Quaternion.Euler(0, - angle/2, 0) * transform.forward;
        Handles.DrawSolidArc(transform.position, Vector3.up, vectorStart, angle, radius);
    }
# endif
}
