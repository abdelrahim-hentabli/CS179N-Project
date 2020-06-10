using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class PlayerController : MonoBehaviour
{
    public Camera mainCamera;

    public GameObject HUDObject;
    private HUD hud;

    public Animator animator;
	public float speed;
	public float jumpForce;
	private float moveInput;

    private int direction;

    //  Dash variables
    public float maxDashCooldown;
    private float dashCooldown;
    private bool canDash;
    public float dashSpeed;
    private float dashTime;
    public float startDashTime;

	public Rigidbody2D body;
    public BoxCollider2D headCollider;

	private bool facingRight = true;

    //private bool isGrounded;
	public bool isGrounded;
    public bool isCrouched;

	public Transform groundCheck;
	public float checkRadius;
	public LayerMask whatIsGround;

	private int extraJumps;
	public int extraJumpsValue;

    public int numOfKeys = 0;

    // Start is called before the first frame update
    void Start()
    {
        hud = HUDObject.GetComponent<HUD>();
        body = GetComponent<Rigidbody2D>();
        headCollider = GetComponent<BoxCollider2D>();
   		extraJumps = extraJumpsValue;
        dashTime = startDashTime;
        dashCooldown = maxDashCooldown;
        canDash = true;
        //Camera stuff
        Vector3 cameraPosition = transform.position;
        cameraPosition.z = mainCamera.transform.position.z;
        mainCamera.transform.position = cameraPosition;
    }

    // Update is called once per frame
    void Update(){
        //Camera stuff
        Vector3 cameraPosition = transform.position;
        cameraPosition.z = mainCamera.transform.position.z;
        mainCamera.transform.position = cameraPosition;
        
        DashCooldownCheck();


        if (Input.GetKeyDown(KeyCode.M))
        {
            save();
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            load();
        }
        if (direction == 0){
            if((Input.GetAxisRaw("Horizontal") > 0) && Input.GetKeyDown("k")){
                direction = 1;
            } else if((Input.GetAxisRaw("Horizontal") < 0) && Input.GetKeyDown("k")) {
                direction = 2;
            } else if(Input.GetKey("s") && isGrounded){
                if(Input.GetKeyDown("s")){
                    isCrouched = true;
                    animator.SetBool("IsCrouching", true);
                    headCollider.enabled = false;
                }
            } else{
                if(Input.GetKeyUp("s") || !isGrounded){
                    isCrouched = false;
                    animator.SetBool("IsCrouching", false);
                    headCollider.enabled = true;
                }
                body.velocity = new Vector2(moveInput * speed, body.velocity.y);
            }
        } else 
        {
            if(canDash == true) {
                Dash();
            } else {
                direction = 0;
            }
        }   

        Jump();
        
    }

    void FixedUpdate(){

    	isGrounded = (Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround) 
            && (body.velocity.y == 0.0f));

    	moveInput = Input.GetAxisRaw("Horizontal");
        animator.SetFloat("Speed", Mathf.Abs(moveInput));
    
    	if(facingRight == false && moveInput > 0){
    		Flip();
    	} else if(facingRight == true && moveInput < 0) {
    		Flip();
    	}
    }

    void Flip(){

    	facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);

        //Alex: Old code, but I'll leave this here in case we need this part for some reason
    	//Vector3 Scaler = transform.localScale;
    	//Scaler.x *= -1;
    	//transform.localScale = Scaler;
    }

    void DashCooldownCheck() {
        if(canDash == false && dashCooldown > 0.0f) {
            dashCooldown -= Time.deltaTime;
        } else if(dashCooldown <= 0.0f) {
            //Debug.Log(canDash);
            //Debug.Log(dashCooldown);
            dashCooldown = maxDashCooldown;
            canDash = true;
        }
    }

    void Dash(){
        if(dashTime <= 0){
            direction = 0;
            dashTime = startDashTime;
            body.velocity = Vector2.zero;
            animator.SetBool("IsDashing", false);
            canDash = false;
        } else { 
            dashTime -= Time.deltaTime;

            if(direction == 1){
                body.velocity = Vector2.right * dashSpeed;
                animator.SetBool("IsDashing", true);
            } else if(direction == 2) {
                body.velocity = Vector2.left * dashSpeed;
                animator.SetBool("IsDashing", true);
            }
        }
        return;
    }

    void Jump(){
        if(isGrounded == true){
            extraJumps = extraJumpsValue;
            animator.SetBool("IsJumping", false);
            animator.SetBool("IsFalling", false);
        }else if(isGrounded == false) {
        	if(body.velocity.y > 0.0f){
        		animator.SetBool("IsJumping", true);
        	}
        	else if(body.velocity.y <= 0.0f){
        		animator.SetBool("IsJumping", false);
        		animator.SetBool("IsFalling", true);
        	}
            //extraJumps = 0;
        }

        if(Input.GetButtonDown("Jump") && extraJumps > 0){
            body.velocity = Vector2.up * jumpForce;
            extraJumps--;
        } else if(Input.GetButtonDown("Jump") && extraJumps == 0 && isGrounded == true) {
            body.velocity = Vector2.up * jumpForce;
            animator.SetBool("IsJumping", true);
        }
    }

    void save()
    {
        string path = Application.dataPath + "/save.sav";
        string saveData = this.transform.position.ToString() + "\n" + hud.currentLevelTime.ToString() + "\n";
        File.WriteAllText(path, saveData);
        hud.save();
    }

    void load()
    {
        Combat combat = this.GetComponent<Combat>();
        combat.currentHealth = combat.maxHealth;
        this.enabled = true;
        hud.currentHealth = hud.maxHealth;
        hud.healthPotions = HUD.HEALTH_POTION_STARTING_AMOUNT;
        string path = Application.dataPath + "/save.sav";
        StreamReader load_file = new StreamReader(path);
        string temp;
        temp = load_file.ReadLine();
        temp = temp.Substring(1, temp.Length - 2);
        string[] tempArray = temp.Split(',');
        Vector3 tempVec = new Vector3(
            float.Parse(tempArray[0]),
            float.Parse(tempArray[1]),
            float.Parse(tempArray[2]));
        this.transform.position = tempVec;
        hud.currentLevelTime = float.Parse(load_file.ReadLine());
        hud.Refresh();
    }
}
