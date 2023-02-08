#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

[CustomEditor(typeof(SpawnMap))]
[CanEditMultipleObjects]
public class MapEditor : Editor
{
    public SpawnMap spawnMap;
    private void OnEnable() 
    {
        spawnMap = FindObjectOfType<SpawnMap>();
    }

    private void OnSceneGUI() 
    {
        SpawnMap s = target as SpawnMap;

        for (int k = 0 ; k < s.levelSO.levels.Length-1; k++)
        {
            if (s.levelSO.levels[k] != null)
            {
                CustomSpawnEnemiesPoint(s, k);
                CustomSpawnSpritesPoint(s, k);
                CustomSpawnPlanePoint(s, k);
            }
        }

    }

    private void CustomSpawnEnemiesPoint(SpawnMap s, int index)
    {
       Vector3[] arrayPoint = s.levelSO.levels[index].positionSpawnEnemiesList;

       // for each line segment we need two indices into the points array:
        // the index to the start and the end point
        int[] segmentIndices = new int[arrayPoint.Length*2];

        // create the points and line segments indices
        int prevIndex = arrayPoint.Length - 1;
        int pointIndex = 0;
        int segmentIndex = 0;

        for (int i = 0; i < s.levelSO.levels[index].positionSpawnEnemiesList.Length; i++)
        {
            Vector3 pos = arrayPoint[i];

            // the index to the start of the line segment
            segmentIndices[segmentIndex] = pointIndex;
            segmentIndex++;

            pointIndex++;
            prevIndex = i;

            // draw a list of indexed dooted line segments
            // draw arrow dir

            // Handles.color = Color.blue;
            Handles.DrawDottedLines(arrayPoint, segmentIndices, 3);
            int nextIndexPoint = i >= arrayPoint.Length - 1 ? 0 : i + 1;
            float distanceToNextPoint = Vector3.Distance(pos, arrayPoint[nextIndexPoint]);
            // Handles.color = Color.yellow;
            for (int j = 0; j <= distanceToNextPoint/4; j += 2)
            {
                Vector3 dir = (arrayPoint[nextIndexPoint] - pos).normalized;
                if (dir != Vector3.zero)
                {
                    Vector3 posOfArrow = pos + dir*j;
                    Handles.ArrowHandleCap(i, posOfArrow, Quaternion.LookRotation(dir), 2f, EventType.Repaint);
                }
            }

            // draw a button point
            Handles.Label(pos, "Level" + index + $"Point Spawn Enemies {i+1}", "TextField");
            // begin check change on editor
            EditorGUI.BeginChangeCheck();
            Vector3 newPos = Handles.PositionHandle(pos, Quaternion.identity);

            if (EditorGUI.EndChangeCheck())
            {
                // update position point
                Undo.RecordObject(s, "Update Spawn Point");
                arrayPoint[i] = newPos;
                EditorUtility.SetDirty(s);
            }
        }
    }

    private void CustomSpawnSpritesPoint(SpawnMap s, int index)
    {
       Vector3[] arrayPoint = s.levelSO.levels[index].positionSpawnSpritesList;

       // for each line segment we need two indices into the points array:
        // the index to the start and the end point
        int[] segmentIndices = new int[arrayPoint.Length*2];

        // create the points and line segments indices
        int prevIndex = arrayPoint.Length - 1;
        int pointIndex = 0;
        int segmentIndex = 0;

        for (int i = 0; i < s.levelSO.levels[index].positionSpawnSpritesList.Length; i++)
        {
            Vector3 pos = arrayPoint[i];

            // the index to the start of the line segment
            segmentIndices[segmentIndex] = pointIndex;
            segmentIndex++;

            pointIndex++;
            prevIndex = i;

            // draw a list of indexed dooted line segments
            // draw arrow dir

            // Handles.color = Color.blue;
            Handles.DrawDottedLines(arrayPoint, segmentIndices, 3);
            int nextIndexPoint = i >= arrayPoint.Length - 1 ? 0 : i + 1;
            float distanceToNextPoint = Vector3.Distance(pos, arrayPoint[nextIndexPoint]);
            // Handles.color = Color.yellow;
            for (int j = 0; j <= distanceToNextPoint/4; j += 2)
            {
                Vector3 dir = (arrayPoint[nextIndexPoint] - pos).normalized;
                if (dir != Vector3.zero)
                {
                    Vector3 posOfArrow = pos + dir*j;
                    Handles.ArrowHandleCap(i, posOfArrow, Quaternion.LookRotation(dir), 2f, EventType.Repaint);
                }
            }

            // draw a button point
            Handles.Label(pos, "Level" + index + $"Point Spawn Sprites {i+1}", "TextField");
            // begin check change on editor
            EditorGUI.BeginChangeCheck();
            Vector3 newPos = Handles.PositionHandle(pos, Quaternion.identity);

            if (EditorGUI.EndChangeCheck())
            {
                // update position point
                Undo.RecordObject(s, "Update Spawn Point");
                arrayPoint[i] = newPos;
                EditorUtility.SetDirty(s);
            }
        }
    }

    private void CustomSpawnPlanePoint(SpawnMap s, int index)
    {
       Vector3[] arrayPoint = s.levelSO.levels[index].positionSpawnPlaneList;

       // for each line segment we need two indices into the points array:
        // the index to the start and the end point
        int[] segmentIndices = new int[arrayPoint.Length*2];

        // create the points and line segments indices
        int prevIndex = arrayPoint.Length - 1;
        int pointIndex = 0;
        int segmentIndex = 0;

        for (int i = 0; i < s.levelSO.levels[index].positionSpawnPlaneList.Length; i++)
        {
            Vector3 pos = arrayPoint[i];

            // the index to the start of the line segment
            segmentIndices[segmentIndex] = pointIndex;
            segmentIndex++;

            pointIndex++;
            prevIndex = i;

            // draw a list of indexed dooted line segments
            // draw arrow dir

            // Handles.color = Color.blue;
            Handles.DrawDottedLines(arrayPoint, segmentIndices, 3);
            int nextIndexPoint = i >= arrayPoint.Length - 1 ? 0 : i + 1;
            float distanceToNextPoint = Vector3.Distance(pos, arrayPoint[nextIndexPoint]);
            // Handles.color = Color.yellow;
            for (int j = 0; j <= distanceToNextPoint/4; j += 2)
            {
                Vector3 dir = (arrayPoint[nextIndexPoint] - pos).normalized;
                if (dir != Vector3.zero)
                {
                    Vector3 posOfArrow = pos + dir*j;
                    Handles.ArrowHandleCap(i, posOfArrow, Quaternion.LookRotation(dir), 2f, EventType.Repaint);
                }
            }

            // draw a button point
            Handles.Label(pos, "Level" + index + $"Point Spawn Planes {i+1}", "TextField");
            // begin check change on editor
            EditorGUI.BeginChangeCheck();
            Vector3 newPos = Handles.PositionHandle(pos, Quaternion.identity);

            if (EditorGUI.EndChangeCheck())
            {
                // update position point
                Undo.RecordObject(s, "Update Spawn Point");
                arrayPoint[i] = newPos;
                EditorUtility.SetDirty(s);
            }
        }
    }
}

#endif
