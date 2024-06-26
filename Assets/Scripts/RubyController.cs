using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubyController : MonoBehaviour
{ 	public int Score;
    public float speed = 3.0f;
    
    public int maxHealth = 5;
    
    public GameObject projectilePrefab;
    public GameObject damagePrefab;
	public GameObject healPrefab;
    public AudioClip throwSound;
    public AudioClip hitSound;
    
    public int health { get { return currentHealth; }}
    int currentHealth;
    
    public float timeInvincible = 2.0f;
    bool isInvincible;
    float invincibleTimer;
    
    Rigidbody2D rigidbody2d;
    float horizontal;
    float vertical;
    
    Animator animator;
    Vector2 lookDirection = new Vector2(1,0);
    
    AudioSource audioSource;
    //dash stuff by Alex Gaylord
    public float dashingDistance = 5f;
    public float dashingTime = 0.5f;
    private Vector2 dashingDir;
    private bool isDashing;
    private bool canDash = true;
    private TrailRenderer trailRenderer;
    public AudioClip dashSound;
	//slow by Shyanne Murdock
    private bool isSlowed = false;
    private float originalSpeed;
    private float slowDuration = 3f;
    private float slowTimer = 0f;
	
    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        
        currentHealth = maxHealth;

        audioSource = GetComponent<AudioSource>();
		/*dash*/ trailRenderer = GetComponent<TrailRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        
        Vector2 move = new Vector2(horizontal, vertical);
        
        if(!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        {
            lookDirection.Set(move.x, move.y);
            lookDirection.Normalize();
        }
        
        animator.SetFloat("Look X", lookDirection.x);
        animator.SetFloat("Look Y", lookDirection.y);
        animator.SetFloat("Speed", move.magnitude);
        
        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer < 0)
                isInvincible = false;
        }
        
        if(Input.GetKeyDown(KeyCode.C))
        {
            Launch();
        }
        
        if (Input.GetKeyDown(KeyCode.X))
        {
            RaycastHit2D hit = Physics2D.Raycast(rigidbody2d.position + Vector2.up * 0.2f, lookDirection, 1.5f, LayerMask.GetMask("NPC"));
            if (hit.collider != null)
            {
                NonPlayerCharacter character = hit.collider.GetComponent<NonPlayerCharacter>();
                if (character != null)
                {
                    character.DisplayDialog();
                }
            }
        }
		//slow 
        if (isSlowed)
        {
            slowTimer += Time.deltaTime;

            // checking slow duration
            if (slowTimer >= slowDuration)
            {

                RemoveSlowEffect();
            }
        }
		/*dash*/ 


        if (Input.GetButtonDown("Dash") && canDash)
        {
            isDashing = true;
            canDash = false;
            trailRenderer.emitting = true;
            dashingDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            StartCoroutine(PerformDash());
        }
    }
    
    void FixedUpdate()
    {
        Vector2 position = rigidbody2d.position;
        position.x = position.x + speed * horizontal * Time.deltaTime;
        position.y = position.y + speed * vertical * Time.deltaTime;

        rigidbody2d.MovePosition(position);
    }
	//slow 
    public void ApplySlowEffect()
    {
        if (!isSlowed)
        {
            originalSpeed = speed; // Store the original speed
            speed *= 0.5f; // Reduce speed to half
            isSlowed = true;
            slowTimer = 0f; // Reset the slow effect duration timer
        }
    }

    public void RemoveSlowEffect()
    {
        if (isSlowed)
        {
            speed = originalSpeed; // Restore the original speed
            isSlowed = false;
        }
    }
//dash stuff below by AG
    private IEnumerator PerformDash()
    {
        PlaySound(dashSound);

        float elapsedTime = 0f;
        Vector2 startPos = transform.position;

        while (elapsedTime < dashingTime)
        {
            float dashProgress = elapsedTime / dashingTime;
            transform.position = Vector2.Lerp(startPos, startPos + dashingDir.normalized * dashingDistance, dashProgress);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        trailRenderer.emitting = false;
        isDashing = false;
        canDash = true; 
    }
    public void ChangeHealth(int amount)
    {
        if (amount < 0)
        {
            if (isInvincible)
                return;
            
            isInvincible = true;
            invincibleTimer = timeInvincible;
             GameObject damageObject = Instantiate(damagePrefab, rigidbody2d.position + Vector2.up * 0.5f, Quaternion.identity);
            PlaySound(hitSound);
        }
		if (amount > 0)
        {
            
             GameObject healObject = Instantiate(healPrefab, rigidbody2d.position + Vector2.up * 0.5f, Quaternion.identity);
            
        }
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        
        UIHealthBar.instance.SetValue(currentHealth / (float)maxHealth);
		if (currentHealth <= 0)
        {
            FindObjectOfType<GameManager>().PlayerLost(); // Notify GameManager when player loses
        }
    }
    
    void Launch()
    {
        GameObject projectileObject = Instantiate(projectilePrefab, rigidbody2d.position + Vector2.up * 0.5f, Quaternion.identity);

        Projectile projectile = projectileObject.GetComponent<Projectile>();
        projectile.Launch(lookDirection, 300);

        animator.SetTrigger("Launch");
        
        PlaySound(throwSound);
    } 
	//Flower Score  made by Shyanne Murdock
    private void OnTriggerEnter(Collider other)
	{	
		if(other.gameObject.tag == "Flower")
		{	
			FlowerScore.scoreCount += 1;
			}
	}
    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
}