using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class GameController : MonoBehaviour
{
    enum State { Normal, Debug, Vision};
    private State state;
    private LineRenderer lineRenderer;
    public GameObject player;
    public GameObject[] pickups;
    public GameObject closestPickup;
    public Vector3 lastPosition;
    public Vector3 startPosition;
    public Vector3 endPosition;
    public Vector3 tempEndPosition;
    public TextMeshProUGUI positionText;
    public TextMeshProUGUI velocityText;
    public TextMeshProUGUI distanceText;
    public TextMeshProUGUI modeText;

    float shortestDistance;

    void Start()
    {
        state = State.Vision;
        closestPickup = FindClosestPickup();
        player = GameObject.Find("Player");
        SetModeText();
        /*lineRenderer = player.AddComponent<LineRenderer>();
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;*/
    }

    void Update()
    {
        closestPickup = FindClosestPickup();
        if (Input.GetKeyDown("space"))
        {
            ChangeState();
            SetModeText();
        }

        if (state == State.Normal)
        {
            Normal();
        }
        else if (state == State.Debug)
        {
            Debug();
        }
        else
        {
            Vision();
        }

       /* startPosition = player.transform.position;
        lineRenderer.SetPosition(0, startPosition);
        findEndPosition();*/
    }

    private void Normal()
    {
        ClearRenderLine();
        SetAllWhite();
    }

    private void Debug()
    {
        if (lineRenderer == null)
        {
            lineRenderer = gameObject.AddComponent<LineRenderer>();
        }
        RenderLine();
        SetLastPosition();
        SetClosestBlue();
        SetOthersWhite();
        SetDistanceText();
        SetVelocityText();
        SetPositionText();
    }

    private void Vision()
    {
        if (lineRenderer == null)
        {
            lineRenderer = gameObject.AddComponent<LineRenderer>();
        }
        RenderLine();
        SetLastPosition();
        SetClosestGreen();
        SetOthersWhite();
        SetDistanceText();
        SetVelocityText();
        SetPositionText();
        closestPickup.transform.LookAt(player.transform);
    }

    public GameObject FindClosestPickup()
    {
        pickups = GameObject.FindGameObjectsWithTag("PickUp");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject pickup in pickups)
        {
            float tempDistance = Vector3.Distance(pickup.transform.position, transform.position);
            if (tempDistance < distance)
            {
                closest = pickup;
                distance = tempDistance;
            }
        }
        return closest;
    }

    public void RenderLine()
    {
        lineRenderer.SetPosition(0, player.transform.position);
        lineRenderer.SetPosition(1, closestPickup.transform.position);
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
    }

    public void ChangeState()
    {
        if (state == State.Normal)
        {
            state = State.Debug;
        }
        else if (state == State.Debug)
        {
            state = State.Vision;
        }
        else
        {
            state = State.Normal;
        }
    }

    public void SetClosestBlue()
    {
        closestPickup.GetComponent<Renderer>().material.color = Color.blue;
    }

    public void SetClosestGreen()
    {
        closestPickup.GetComponent<Renderer>().material.color = Color.green;
    }

    public void SetOthersWhite()
    {
        foreach (GameObject pickup in pickups)
        {
            if (pickup != closestPickup)
            {
                pickup.GetComponent<Renderer>().material.color = Color.white;
            }
        }
    }

    public void SetAllWhite()
    {
        foreach(GameObject pickup in pickups)
        {
            pickup.GetComponent<Renderer>().material.color = Color.white;
        }
    }

    private void SetModeText()
    {
        if (state == State.Normal)
        {
            modeText.text = "Mode: Normal";
        }
        else if (state == State.Debug)
        {
            modeText.text = "Mode: Debug";
        }
        else
        {
            modeText.text = "Mode: Vision";
        }
    }

    private void SetPositionText()
    {
        positionText.text = transform.position.ToString("0.0");
    }

    private void SetVelocityText()
    {
        float velocity = Vector3.Distance(transform.position, lastPosition) / Time.deltaTime;
        velocityText.text = velocity.ToString("0.0");
    }

    private void SetDistanceText()
    {

        distanceText.text = Vector3.Distance(transform.position, closestPickup.transform.position).ToString("0.0");
    }

    public void SetLastPosition()
    {
        lastPosition = transform.position;
    }

    private void ClearText()
    {
        positionText.text = "";
        velocityText.text = "";
        distanceText.text = "";
    }

    private void ClearRenderLine()
    {
        lineRenderer.startWidth = 0.0f;
        lineRenderer.endWidth = 0.0f;
    }


    
  /*  void findEndPosition()
    {
        pickups = GameObject.FindGameObjectsWithTag("PickUp");
        shortestDistance = Mathf.Infinity;


        for (var i = 0; i < pickups.Length; i++)
        {
            var distance = Vector3.Distance(pickups[i].transform.position, player.transform.position);
            pickups[i].GetComponent<Renderer>().material.color = Color.white;

            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                endPosition = pickups[i].transform.position;
                lineRenderer.SetPosition(1, endPosition);
                pickups[i].GetComponent<Renderer>().material.color = Color.blue;
                closestPickUp.text = pickups[i].name; //Vector3.Distance(pickups[i].transform.position, player.transform.position).ToString();
            }
        }
    }

  /*  void setColors()
    {
        pickups = GameObject.FindGameObjectsWithTag("PickUp");
        for (var i = 0; i < pickups.Length; i++)
        {
            pickups[i].GetComponent<Renderer>().material.color = Color.white;
        }
    }*/




}
