using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Driver : MonoBehaviour
{
    [SerializeField] float steerSpeed = 200f; 
    [SerializeField] float origSpeed = 18f;
    [SerializeField] float moveSpeed = 18f; // the [SerializeField] allows this var to be changed in unity
    [SerializeField] float slowSpeed = 14f;
    [SerializeField] float boostSpeed = 20f;

    void Update() {
        float steerAmount = Input.GetAxis("Horizontal") * steerSpeed * Time.deltaTime;
        float moveAmount = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;


        transform.Rotate(0, 0, -steerAmount);
        transform.Translate(0,moveAmount,0);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        moveSpeed = slowSpeed; 
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Speed Up") {
                moveSpeed = boostSpeed;
                other.tag = "Used";
                var sprtOfOther = other.GetComponent<SpriteRenderer>();
                sprtOfOther.color = new Color32(1, 1, 1, 1);
        }
    }
}