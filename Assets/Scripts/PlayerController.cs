using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float MoveSpeed = 6;
    public float PlayerSize = 1.5f;
    CapsuleCollider2D humanCollider;
    BoxCollider2D ratCollider;
    GameObject humanModel;
    GameObject ratModel;
    // Start is called before the first frame update
    void Start()
    {
        humanCollider = GetComponent<CapsuleCollider2D>();
        humanCollider.enabled = true;
        ratCollider = GetComponent<BoxCollider2D>();
        ratCollider.enabled = false;
        humanModel = transform.GetChild(0).gameObject;
        humanModel.SetActive(true);
        ratModel = transform.GetChild(1).gameObject;
        ratModel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        updateMovement();
    }

    void updateMovement() {
        if (Input.GetKey(KeyCode.W)) {
            transform.Translate(Vector2.up * MoveSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A)) {
            Flip(true);
            transform.Translate(Vector2.left * MoveSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S)) {
            transform.Translate(Vector2.down * MoveSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D)) {
            Flip(false);
            transform.Translate(Vector2.right * MoveSpeed * Time.deltaTime);
        }
    }

    protected void Flip(bool bLeft) {
        transform.localScale = new Vector3(bLeft ? PlayerSize : -PlayerSize, PlayerSize, PlayerSize);
    }
    private void playerTransform(bool toRat) {
        if (toRat) {
            humanCollider.enabled = false;
            humanModel.SetActive(false);
            ratCollider.enabled = true;
            ratModel.SetActive(true);
        } else {
            humanCollider.enabled = true;
            humanModel.SetActive(true);
            ratCollider.enabled = false;
            ratModel.SetActive(false);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("RatChange")) {
            playerTransform(true);
            Destroy(collision.gameObject);
        }
        if (collision.CompareTag("HumanChange")) {
            playerTransform(false);
            Destroy(collision.gameObject);
        }
    }
}
