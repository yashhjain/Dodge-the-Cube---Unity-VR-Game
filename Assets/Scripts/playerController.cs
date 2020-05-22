using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class playerController : MonoBehaviour
{
    public CharacterController controller;
    public GameObject bullet;
    public float speed = 5.0f;
    public float verticalVelocity = 0.0f;
    public float gravity = 12.0f;
    private float jump = 5.0f;
    private Vector3 moveVector;
    private string HORIZONTAL = "Horizontal";
    private string VERTICAL = "Vertical";
    private bool isDead = false;
    private float animationTime = 1.0f;
    public int health = 100;
    private bool didItCollide = false;
    private string RED = "Cube-Red";
    private string BLUE = "Cube-Blue";
    private string GREEN = "Cube-Green";
    private string HEALTHKIT = "box_med";
    private string HEALTH = "Health: ";
    public string HIGHSCORE = "High Score";
    public Text healthBarText;
    public Text highScoreRef;
    
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        healthBarText.text = HEALTH + health.ToString();
        highScoreRef.text = "High Score: " + PlayerPrefs.GetInt(HIGHSCORE);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) { 
            GameObject bulletObject = Instantiate(bullet);
            bulletObject.transform.position = transform.position + transform.forward;
            // bulletObject.transform.forward = transform.forward;
        }
        
        if(isDead)
            return;
        
        if(Time.time < animationTime){
            controller.Move(Vector3.forward * speed * Time.deltaTime);
            return;
        }
        
        moveVector = Vector3.zero;
        
        if(controller.isGrounded){
            verticalVelocity = 0.53f;
            if (Input.GetButton("Jump")) {
                verticalVelocity = jump;
            }
        }
        else{
            verticalVelocity -= gravity * Time.deltaTime;
        }
        moveVector.x = Input.GetAxisRaw(HORIZONTAL) * speed;
        moveVector.y = verticalVelocity;
        moveVector.z = speed;
        controller.Move(moveVector * Time.deltaTime);

        //Also, Check if the player reached the end of the floor.
        if(transform.position.x <= -5.0f || transform.position.x >= 5.0f){
            this.Death();
        }
        healthBarText.text = HEALTH + health.ToString();
        didItCollide = false;
        if(health <= 0){
            health = 0;
            healthBarText.text = HEALTH + health.ToString();
            Death();
        }
    }
    
    public bool getIsDead(){
        return isDead;
    }

    public void ChangeSpeed(float change){
        speed = 5.0f + change;
    }

    private void OnTriggerEnter(Collider collider){
        if(collider.gameObject.name.Contains("Cube") == true){
            Debug.Log("Collision Occurred");
        }
    }

    private void CubeDamage(ControllerColliderHit hit, int damage){
        if(health <= 0){
            health = 0;
            Death();
        }
        if(didItCollide){
            return;
        }
        didItCollide = true;
        Destroy(hit.collider.gameObject);
        health -= damage;
    }

    private void fillUpHealth(ControllerColliderHit hit){
        health = 100;
        healthBarText.text = HEALTH + health.ToString();
        Destroy(hit.collider.gameObject);
    }

    private void OnControllerColliderHit(ControllerColliderHit hit){
        if(hit.gameObject.name.Contains(RED) == true){
            CubeDamage(hit, 40);
        }
        else if(hit.gameObject.name.Contains(BLUE) == true){
            CubeDamage(hit, 35);
        }
        else if(hit.gameObject.name.Contains(GREEN) == true){
            CubeDamage(hit, 25);
        }
        else if(hit.gameObject.name.Contains(HEALTHKIT) == true){
            fillUpHealth(hit);
        }
    }

    private void Death(){
        isDead = true;
        GetComponent<Score>().OnDeath();
        GetComponent<Score>().setGameOver();
        Debug.Log("Dead");
    }
}
