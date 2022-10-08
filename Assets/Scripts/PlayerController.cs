using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    
    public Vector2 moveValue;
    public float speed;
    private int count;
    private int numPickups = 7;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI winText;
    public TextMeshProUGUI playerPosition;
    public TextMeshProUGUI playerVelocity;
    private Vector3 initPosition;
    
    void Start()
    {
        count = 0;
        winText.text = "";
        SetCountText();
        initPosition = transform.position;
    }

    void OnMove(InputValue value)
    {
        moveValue = value.Get<Vector2>();
    }

    void FixedUpdate()
        {
            Vector3 movement = new Vector3(moveValue.x, 0.0f, moveValue.y);

            GetComponent<Rigidbody>().AddForce(movement * speed * Time.fixedDeltaTime);

        SetPositionText();
        SetVelocityText();
        initPosition = transform.position;
             }
    
    void Update()
    {
        //initPosition = transform.position;
    }

    void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.tag == "PickUp")
            {
                other.gameObject.SetActive(false);
                count++;
                SetCountText();
            }
        }
    private void SetCountText()
    {
        scoreText.text = "Score: " + count.ToString();
        if(count >= numPickups)
        {
            winText.text = "You win!";
        }
    }
    private void SetPositionText()
    {
        Vector3 position = transform.position;
        string positionText = position.ToString();
        playerPosition.text = positionText;
    }
    private void SetVelocityText()
    {
        Vector3 position = transform.position;
        Vector3 velocity = (position - initPosition) / Time.deltaTime;
        playerVelocity.text = velocity.ToString();
    }
}
