using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    private LineRenderer lineRenderer;
    public TextMeshProUGUI playerPosition;
    public TextMeshProUGUI playerVelocity;

    void Start()
    {
        lineRenderer = GameObject.AddComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
