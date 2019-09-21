using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;

    private Rigidbody rig;
    private AudioSource audioSource;

    private void Awake() {
        rig = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update() {
        Move();

        if (Input.GetButtonDown("Jump")) {
            TryJump();
        }
    }

    private void Move() {
        float xInput = Input.GetAxis("Horizontal");
        float zInput = Input.GetAxis("Vertical");

        Vector3 dir = new Vector3(xInput, 0, zInput) * moveSpeed;
        dir.y = rig.velocity.y;

        rig.velocity = dir;

        SetFacing(xInput, zInput);
    }

    private void SetFacing(float xInput, float zInput) {
        Vector3 facingDir = new Vector3(xInput, 0, zInput);

        if (facingDir.magnitude > 0) {
            transform.forward = facingDir;
        }
    }

    private void TryJump() {
        if( TouchingGround() ) {
            rig.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    private bool TouchingGround() {
        Ray ray1 = new Ray(transform.position + new Vector3(0.5f, 0, 0.5f), Vector3.down);
        Ray ray2 = new Ray(transform.position + new Vector3(0.5f, 0, -0.5f), Vector3.down);
        Ray ray3 = new Ray(transform.position + new Vector3(-0.5f, 0, -0.5f), Vector3.down);
        Ray ray4 = new Ray(transform.position + new Vector3(-0.5f, 0, 0.5f), Vector3.down);
        
        return Physics.Raycast(ray1, 0.7f) || Physics.Raycast(ray2, 0.7f) || Physics.Raycast(ray3, 0.7f) || Physics.Raycast(ray4, 0.7f);
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Enemy")) {
            GameManager.instance.GameOver();
        } else if (other.CompareTag("Coin")) {
            GameManager.instance.AddScore(1);
            Destroy(other.gameObject);
            audioSource.Play();
        } else if (other.CompareTag("Goal")) {
            GameManager.instance.LevelEnd();
        }
    }
}
