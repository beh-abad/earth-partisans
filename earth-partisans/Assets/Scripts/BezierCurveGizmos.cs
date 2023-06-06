//This code has been witten by Behrang Abad Foomani
using UnityEngine;

public class BezierCurveGizmos : MonoBehaviour
{   
    public Transform[] gizmosBasePoints;   
    private void OnDrawGizmos()
    {      
        for (float t = 0; t <= 1; t += 0.05f)
        {
           Vector2 gizmosPoint = Mathf.Pow(1 - t, 3) * gizmosBasePoints[0].position + 3 * Mathf.Pow(1 - t, 2) * t * gizmosBasePoints[1].position +
                3 * (1 - t) * Mathf.Pow(t, 2) * gizmosBasePoints[2].position + Mathf.Pow(t, 3) * gizmosBasePoints[3].position;
            Gizmos.DrawSphere(gizmosPoint, 0.05f);
        }
        Gizmos.DrawLine(new Vector2(gizmosBasePoints[0].position.x, gizmosBasePoints[0].position.y), new Vector2(gizmosBasePoints[1].position.x, gizmosBasePoints[1].position.y));
        Gizmos.DrawLine(new Vector2(gizmosBasePoints[2].position.x, gizmosBasePoints[2].position.y), new Vector2(gizmosBasePoints[3].position.x, gizmosBasePoints[3].position.y));
    }
}
