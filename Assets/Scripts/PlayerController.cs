using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float MoveSpeed = 6;
    public float PlayerSize = 1.5f;
    CapsuleCollider2D humanCollider;
    BoxCollider2D ratCollider;
    GameObject humanModel;
    GameObject ratModel;
    Animator anim;
    bool isHuman;
    
    [SerializeField] private AudioSource doorSFX;
    // Start is called before the first frame update
    void Start()
    {
        isHuman = true;
        humanCollider = GetComponent<CapsuleCollider2D>();
        humanCollider.enabled = true;
        ratCollider = GetComponent<BoxCollider2D>();
        ratCollider.enabled = false;
        humanModel = transform.GetChild(0).gameObject;
        humanModel.SetActive(true);
        ratModel = transform.GetChild(1).gameObject;
        ratModel.SetActive(false);
        anim = humanModel.GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        updateMovement();
    }

    void updateMovement() {
        if (Input.GetKey(KeyCode.W)) {
            transform.Translate(Vector2.up * MoveSpeed * Time.deltaTime);
            if (isHuman) {
                anim.Play("Run");
            }
        }
        if (Input.GetKey(KeyCode.A)) {
            Flip(true);
            transform.Translate(Vector2.left * MoveSpeed * Time.deltaTime);
            if (isHuman) {
                anim.Play("Run");
            }
        }
        if (Input.GetKey(KeyCode.S)) {
            transform.Translate(Vector2.down * MoveSpeed * Time.deltaTime);
            if (isHuman) {
                anim.Play("Run");
            }
        }
        if (Input.GetKey(KeyCode.D)) {
            Flip(false);
            transform.Translate(Vector2.right * MoveSpeed * Time.deltaTime);
            if (isHuman) {
                anim.Play("Run");
            }
        }
        if (isHuman && Input.GetKey(KeyCode.Space)) {
            anim.Play("Attack");
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
            isHuman = false;
        }
        if (collision.CompareTag("HumanChange")) {
            playerTransform(false);
            Destroy(collision.gameObject);
            isHuman = true;
        }
        if (collision.CompareTag("ToLevel0") && isHuman) {
            SceneManager.LoadScene("level0");
        }
        if (collision.CompareTag("ToLevel1") && isHuman) {
            SceneManager.LoadScene("level1");
        }
        if (collision.CompareTag("ToLevel2") && isHuman) {
            SceneManager.LoadScene("level2");
        }
        if (collision.CompareTag("ToEndScene") && isHuman) {
            SceneManager.LoadScene("endScene");
        }
        if(collision.CompareTag("lvl1wire") && !isHuman && GameObject.Find("lvl1door").gameObject){
            doorSFX.Play();
            Destroy(collision.gameObject);
            Destroy(GameObject.Find("lvl1door").gameObject);
        }
    }

    private void attack() {
        anim.Play("Attack");
    }
}
