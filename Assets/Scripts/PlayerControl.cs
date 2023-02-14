using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float MoveSpeed = 6;
    public float playerSize = 1.5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        updateMovement();
    }

    void updateMovement() {
        if (Input.GetKey(KeyCode.W)) {
            transform.transform.Translate(Vector2.up * MoveSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A)) {
            Flip(true);
            transform.transform.Translate(Vector2.left * MoveSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S)) {
            transform.transform.Translate(Vector2.down * MoveSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D)) {
            Flip(false);
            transform.transform.Translate(Vector2.right * MoveSpeed * Time.deltaTime);
        }
    }

    protected void Flip(bool bLeft) {
        transform.localScale = new Vector3(bLeft ? playerSize : -playerSize, playerSize, playerSize);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("RatChange")) {
            playerSize = 1f;
            Flip(false);
            Destroy(collision.gameObject);
        }
        if (collision.CompareTag("HumanChange")) {
            playerSize = 1.5f;
            Flip(false);
            Destroy(collision.gameObject);
        }
    }
}
