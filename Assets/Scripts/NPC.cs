using System;
using Narrative_Engine;
using UnityEngine;
using Random = UnityEngine.Random;

public class NPC : Interactable
{
    [Header("Wandering")]
    public bool canWander;
    public bool interacting;
    public float wanderingCooldown;
    public float minWanderingTimer;
    public float maxWanderingDistance;
    public float movementSpeed;
    private Vector2 startingPosition;
    private Vector2 wanderingDirection;
    private float wanderingTimer;
    private bool moving;
    private Dialog genericDialog;

    
    private bool needsToTeleport = false;
    private Place placeToTeleport;
    
    private Rigidbody2D body;

    public Dialog GenericDialog { get => genericDialog; set => genericDialog = value; }

    public Place place;

    protected new void Start()
    {
        wanderingTimer = Random.Range(0f, wanderingCooldown);
        startingPosition = transform.position;
        wanderingDirection = Vector2.zero;
        interacting = false;
        moving = false;
        body = GetComponent<Rigidbody2D>();
        base.Start();
    }

    public override void Interact()
    {
        if(genericDialog != null)
        {
            DialogManager.GetInstance().StartDialog(genericDialog, 0, gameObject);
        }
        else Debug.Log("No generic dialog");
    }

    public virtual void Update()
    {
        WanderingBehaviour();
    }

    private void WanderingBehaviour()
    {
        if (canWander && !interacting)
        {
            wanderingTimer -= Time.deltaTime;
            if (wanderingTimer < 0)
            {
                if (moving)
                {
                    moving = false;
                }
                else
                {
                    moving = true;
                    NewWanderingDirection();
                }
                wanderingTimer = Random.Range(minWanderingTimer, wanderingCooldown);
                // TODO: actualizar esto para que ande en una dirección durante X tiempo
                // o hasta que colisione con un objeto. Averiguar cómo hacer para que
                // el jugador no lo empuje (kinematic?)
            }
        }
    }

    private void NewWanderingDirection()
    {
        switch(Random.Range(0, 4))
        {
            case 0: // Up
                wanderingDirection = Vector2.up;
                break;
            case 1: // Right
                wanderingDirection = Vector2.right;
                break;
            case 2: // Down
                wanderingDirection = Vector2.down;
                break;
            case 3: // Left
                wanderingDirection = Vector2.left;
                break;
        }
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        if (canWander && moving && !interacting)
        {
            float distance = Vector2.Distance(transform.position, startingPosition);
            if(distance >= maxWanderingDistance)
            {
                wanderingDirection = -wanderingDirection;
            }
            body.MovePosition((Vector2)transform.position 
                + wanderingDirection 
                * (movementSpeed * Time.fixedDeltaTime));
        }
    }

    public new void OnCollisionEnter2D(Collision2D collision)
    {
        if (canWander)
        {
            if (collision.gameObject.GetComponent<PlayerMovement>())
            {
                interacting = true;
            }
            else
            {
                wanderingDirection = -wanderingDirection;
            }
        }
        base.OnCollisionEnter2D(collision);
    }

    public new void OnCollisionExit2D(Collision2D collision)
    {
        if (canWander)
        {
            if (collision.gameObject.GetComponent<PlayerMovement>())
            {
                interacting = false;
            }
        }
        base.OnCollisionExit2D(collision);
    }
    
    public virtual void DialogEnded(bool success){}

    public void Teleport(Place placeToTeleport)
    {
        needsToTeleport = true;
        this.placeToTeleport = placeToTeleport;
    }
    
    
    private void OnBecameInvisible()
    {
        if (!NeedsToTeleport())
            return;

        gameObject.transform.position = placeToTeleport.validCoordinates[0];
        placeToTeleport.validCoordinates.RemoveAt(0);

        place.characters.Remove(gameObject);
        place = placeToTeleport;
        placeToTeleport = null;
    }
    
    
    protected virtual bool NeedsToTeleport()
    {
        return needsToTeleport;
    }
}
