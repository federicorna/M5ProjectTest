
using System.Collections;
using UnityEngine;

public class LinerendererHandler : MonoBehaviour
{
    [SerializeField] private int _subdivisions = 12;
    [SerializeField] private float _circleRadius = 1.5f;
    [SerializeField] private LayerMask _whatIsObstacle;
    [SerializeField] private float _interval = 0.5f;

    private LineRenderer _lineRenderer;

    private CharacterDetection _characterDetection;


    void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();

        _characterDetection = GetComponent<CharacterDetection>();
        EvaluateConeOfViewWithSinAndCos(_subdivisions);
    }


    private void OnEnable()
    {
        StartCoroutine(CustomUpdate());
    }


    private IEnumerator CustomUpdate()
    {
        while (true)
        {
            yield return new WaitForSeconds (_interval);
            EvaluateConeOfViewWithSinAndCos (_subdivisions);
        }
    }


//  Versione con sin e cos, altrimenti si puo fare con quaternioni che forse piu intuitivo!!!!
    public void EvaluateConeOfViewWithSinAndCos (int subdivisions)
    {
        float angle = (90 - _characterDetection.ViewAngle) * Mathf.Deg2Rad;

        int points = subdivisions + 1;

        _lineRenderer.positionCount = points;

        Vector3[] positions = new Vector3[points];

        float deltaAngle = (2 * _characterDetection.ViewAngle / subdivisions) * Mathf.Deg2Rad;

        for (int i = 0; i < subdivisions; i++)
        {
            float currentAngles = angle + i * deltaAngle;
            positions[i].x = Mathf.Cos (currentAngles) * _characterDetection.SightDistance;
            positions[i].z = Mathf.Sin (currentAngles) * _characterDetection.SightDistance;
        }

        positions[subdivisions].x = 0;
        positions[subdivisions].z = 0;

        _lineRenderer.SetPositions (positions);
    }



    public void EvaluateCircle (int subdivisions)
    {
        _lineRenderer.positionCount = subdivisions;

        Vector3[] positions = new Vector3[subdivisions];

        float deltaAngle = Mathf.PI * 2 / subdivisions;

        for (int i = 0; i < subdivisions; i++)
        {
            float currentAngles = i * deltaAngle;
            positions[i].x = Mathf.Cos(currentAngles) * _circleRadius;
            positions[i].z = Mathf.Sin(currentAngles) * _circleRadius;
        }

        _lineRenderer.SetPositions(positions);
    }
}
