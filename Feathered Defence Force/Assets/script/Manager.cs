using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    [SerializeField] List<Transform> waypoints = new List<Transform>();
    public static List<Transform> globalWaypoints = new List<Transform>();

    [SerializeField] AnimationCurve curve;
    public static AnimationCurve globalCurve;


    private void Awake()
    {
        globalWaypoints = waypoints;
        globalCurve = curve;
    }
    private void Start()
    {
        
    }
    private void Update()
    {
        
    }
}
