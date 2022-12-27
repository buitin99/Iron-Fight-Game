#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(EnemyController))]
[CanEditMultipleObjects]
public class EnemyEditor : Editor
{
    bool showToggle;
    private void OnSceneGUI() {
        Enemy t = target as Enemy;
        if(t.patrolList != null) {
            CustomPatrolPoint(t);
            if(t.typePatrol == Enemy.TypePatrol.STANDINPLACE) {
                CustomStandPoint(t);
            }
        }
    }
    public override void OnInspectorGUI() {
        base.OnInspectorGUI();
        Enemy enemyBehaviour = target as Enemy;
        if(enemyBehaviour.typePatrol == Enemy.TypePatrol.STANDINPLACE) {
            EditorGUI.BeginChangeCheck();
            Vector3 standPos = EditorGUILayout.Vector3Field("Stand Position",enemyBehaviour.standPos);
            
            if(EditorGUI.EndChangeCheck()) {
                Undo.RecordObject(enemyBehaviour, "Update stand point");
                enemyBehaviour.standPos = standPos;
                EditorUtility.SetDirty(enemyBehaviour);
            }
        }
    }

    private void CustomPatrolPoint(Enemy t) {
        Vector3[] listPoint = t.patrolList;

        // for each line segment we need two indices into the points array:
        // the index to the start and the end point
        int[] segmentIndices = new int[listPoint.Length * 2];

        // create the points and line segments indices
        int prevIndex = listPoint.Length - 1;
        int pointIndex = 0;
        int segmentIndex = 0;

        for(int i = 0; i< t.patrolList.Length; i++) {
            Vector3 pos = listPoint[i];

            // the index to the start of the line segment
            segmentIndices[segmentIndex] = prevIndex;
            segmentIndex++;

            // the index to the end of the line segment
            segmentIndices[segmentIndex] = pointIndex;
            segmentIndex++;

            pointIndex++;
            prevIndex = i;
                
            //Draw a list of indexed dotted line segments

            if(t.typePatrol == Enemy.TypePatrol.MOVEAROUND) {
                // Draw arrow dir if type patrol is move around
                Handles.color = Color.blue;
                Handles.DrawDottedLines(listPoint, segmentIndices, 3);
                int nextIndexPoint = i >= listPoint.Length - 1 ? 0 : i + 1;
                float distanceToNextPoint = Vector3.Distance(pos, listPoint[nextIndexPoint]);
                Handles.color = Color.yellow;
                for(int j = 0; j <= distanceToNextPoint/4; j += 2) {
                    Vector3 dir = (listPoint[nextIndexPoint] - pos).normalized;
                    if(dir != Vector3.zero) {
                        Vector3 posOfArrow = pos + dir * j;
                        Handles.ArrowHandleCap(i,posOfArrow ,Quaternion.LookRotation(dir), 2f, EventType.Repaint);
                    }
                }
            } else {
                // Draw arrow dir if type patrol is stand in place
                Quaternion rot = Quaternion.LookRotation((pos - t.transform.position).normalized);
                Handles.color = Color.yellow;
                Handles.ArrowHandleCap(i, t.transform.position, rot, 5f, EventType.Repaint);
            }

            //Draw a button point
            Handles.Label(pos, $"Point {i+1}","TextField");

            //begin check change on editor
            EditorGUI.BeginChangeCheck();
            Vector3 newPos = Handles.PositionHandle(pos, Quaternion.identity);

            if(EditorGUI.EndChangeCheck()) {
                // update position point
                Undo.RecordObject(t, "Update Patrol point");
                listPoint[i] = newPos;
                EditorUtility.SetDirty(t);

            }
        }
    }

    private void CustomStandPoint(Enemy t) {
        Handles.Label(t.standPos,"Stand Pos","TextField");
        Handles.color = Color.blue;
        Handles.DrawDottedLine(t.standPos, t.transform.position,2);
        EditorGUI.BeginChangeCheck();
        Vector3 newPos = Handles.PositionHandle(t.standPos, Quaternion.identity);
        if(EditorGUI.EndChangeCheck()) {
            Undo.RecordObject(t, "Update stand point");
            t.standPos = newPos;
            EditorUtility.SetDirty(t);
        }
    }
}
#endif
