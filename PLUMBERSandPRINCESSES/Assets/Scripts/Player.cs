using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [Header("Stats")]
    public float moveSpeed;
    public int atk;
    public int hp;
    public int hpPool;
    public float interactRange;
    public List<string> inventory = new List<string>();

    [Header("Experience")]
    public int lvl;
    public int xp;
    public int xpToNextLvl;
    public float lvlXPMod;

    [Header("Combat")]
    public KeyCode attackKey;
    public float attackRange;
    public float attackRate;
    private float lastAttackTime;

    private Vector2 face;
    [Header("Sprites")]
    public Sprite downFace;
    public Sprite upFace;
    public Sprite leftFace;
    public Sprite rightFace;

    //components
    private Rigidbody2D rig;
    private SpriteRenderer sr;
    private ParticleSystem hitFX;
    private PlayerUI ui;





    private void Awake()
    {
        //get component
        rig = GetComponent<Rigidbody2D>();

        sr = GetComponent<SpriteRenderer>();

        hitFX = gameObject.GetComponentInChildren<ParticleSystem>();

        ui = FindObjectOfType<PlayerUI>();
    }

    // Start is called before the first frame update
    void Start()
    {
        ui.UpdateHp();
        ui.UpdateXp();
        ui.UpdateLvlTxt();
    }

    // Update is called once per frame
    void Update()
    {
        Move();

        if(Input.GetKeyDown(attackKey))
        {
            if(Time.time - lastAttackTime >= attackRate)
            {
                Attack();
            }
        }

        CheckInteract();
    }





    void Move()
    {
        //get horiz and vert input
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        //to calc velocity
        Vector2 vel = new Vector2(x, y);

        //if the player is moving, face in direction of input
        if(vel.magnitude != 0)
        {
            face = vel;
            UpdateSpriteFace();
        }

        //set velocity
        rig.velocity = vel * moveSpeed;
    }

    void CheckInteract()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, face, interactRange, 1 << 9);

        if (hit.collider != null)
        {
            Interactable interactable = hit.collider.GetComponent<Interactable>();
            ui.SetInteractTxt(hit.collider.transform.position, interactable.interactDescr);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                interactable.Interact();
            }

        }
        else
        {
            ui.DisableInteractTxt();
        }

    }

    void UpdateSpriteFace()
    {
        if(face == Vector2.up)
        {
            sr.sprite = upFace;
        } else if (face == Vector2.down)
        {
            sr.sprite = downFace;
        } else if (face == Vector2.left)
        {
            sr.sprite = leftFace;
        } else if (face == Vector2.right)
        {
            sr.sprite = rightFace;
        }
    }

    void Attack()
    {
        lastAttackTime = Time.time;

        //creating a raycast line that will trigger instead of measure
        //origin, direction, max distance, layer mask (used to hit only a specific object layer [enemy has layer id of 8])
        RaycastHit2D hit = Physics2D.Raycast(transform.position, face, attackRange, 1 << 8);

        //if there is a collision, commit atk
        if (hit.collider != null)
        {
            hit.collider.GetComponent<Enemy>().TakeDamage(atk);
        }
    }

    public void TakeDamage(int dmg)
    {
        hitFX.Play();
        hp -= dmg;
        ui.UpdateHp();

        if (hp <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        SceneManager.LoadScene("Game");
    }
    public void AddXP(int pts)
    {
        xp += pts;

        ui.UpdateXp();

        if (xp >= xpToNextLvl)
        {
            LvlUp();
            ui.UpdateXp();
        }
    }

    void LvlUp()
    {
        xp -= xpToNextLvl;
        lvl++;

        xpToNextLvl = Mathf.RoundToInt((float)xpToNextLvl * lvlXPMod);
        ui.UpdateLvlTxt();
    }

    public void AddToInventory(string item)
    {
        inventory.Add(item);
        ui.UpdateInventoryTxt();
    }
}
