using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class playerMovement : MonoBehaviour
{
    
    public float speed;
    float moveinput;
    public int MaxHealth;
    int currenthealth;

    Rigidbody2D rb;
    Animator Animator;
    public Animator animator;
    public Audio_Manager audio_Manager;
    public TextMeshProUGUI textMesh;

    bool facingright = true;
    bool isMoving = false; 


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        currenthealth = MaxHealth;
    }

    

    public void TakeDamage()
    {
        currenthealth -= 1;
    }

    private void FixedUpdate()
    {
        textMesh.text = "HP = " + currenthealth;

        if(currenthealth <= 0)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);


        moveinput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveinput * speed, rb.velocity.y);

        if (rb.velocity.magnitude != 0)
        {
            isMoving = true;
        }
        else 
        { 
            isMoving = false;
        }

        if (isMoving)
        {
            animator.SetBool("ismoving", true);
        }
        else
        {
            animator.SetBool("ismoving", false);
        }

        if (facingright == false && moveinput > 0)
        {
            flip();
        }
        else if (facingright == true && moveinput < 0)
        {
            flip();
        }
    }

    void flip()
    {
        facingright = !facingright;
        transform.Rotate(0f, 180f, 0f);
    }
}
