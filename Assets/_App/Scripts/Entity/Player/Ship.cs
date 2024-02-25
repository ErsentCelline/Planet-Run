using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    [SerializeField] private float _speed;

    // TODO - remove static references.
    public static Ship Instance          { get; private set; }
    public static Transform   Transform  { get; private set; }
    public static Rigidbody2D Body       { get; private set; }
    public static SpaceObject Planet     { get; private set; }
    public static SpaceObject LastPlanet { get; private set; }

    public bool InOrbit { get => Planet != null; }

    // TODO remove public static event.
    public static System.Action<SpaceObject> OnShipArriveToPlanet;
    public static System.Action OnShipDisabled;

    public float angle = 0;

    private bool InGame = false;

    private void Awake()
    {
        Body = GetComponent<Rigidbody2D>();
        Transform = transform;
        Instance = this;
    }

    private void Start()
    {
        // TODO Remove static transition, the ship should not know about the start of the transition animation

        /*
        OnShipDisabled += Transition.Action;
        Transition.OnTransitionEnd += delegate
        {
            if (gameObject.activeInHierarchy)
            {
                InGame = true;
            }
        };
        */
    }

    private static float Lerp = 0;
    private static float Attraction = 0;
    private static bool FlyToTheRight = false;

    public static void OnEnterTheOrbit(SpaceObject planet)
    {
        Planet = planet;
        LastPlanet = planet;

        FlyToTheRight = Planet.transform.position.x > ((Transform.position.x + Transform.up.x) * 6);

        SetAttraction();

        OnShipArriveToPlanet?.Invoke(planet);
    }

    public static void ExitTheOrbit()
    {
        if (Planet != null)
            Planet = null;

        // TODO what is message?..

        Message.HideMessage();
        Lerp = 0;
    }

    private static void SetAttraction()
    {
        Attraction = FlyToTheRight ? Planet.Attraction : Planet.Attraction * -1;

        if (Planet.GetType() == typeof(Planet))
        {
            Attraction *= -1;
        }
    }

    private void Update()
    {
        // TODO Remove "InGame" logic
        if (!InGame) return;

        if (transform.position.x < ScreenHandler.MinScreenX - 2f ||
            transform.position.x > ScreenHandler.MaxScreenX + 2f ||
            transform.position.y > ScreenHandler.MaxScreenY + 3f ||
            transform.position.y < ScreenHandler.MinScreenY - 3f)
        {
            gameObject.SetActive(false);
        }
    }

    private void FixedUpdate()
    {
        // TODO Remove "InGame" logic
        if (!InGame) return;

        transform.position = Vector3.MoveTowards(transform.position, transform.position + Transform.up, _speed * Time.fixedDeltaTime);

        if (Planet != null)
        {
            if (Lerp < 1)
                Lerp += (Time.fixedDeltaTime / 2);
            if (Lerp > 1)
                Lerp = 1f;
            
            Vector3 oppositeCorner = GetOppositCorner(FlyToTheRight).normalized;
            angle = Mathf.Atan2(oppositeCorner.y, oppositeCorner.x) * Mathf.Rad2Deg;

            Body.rotation = Mathf.Lerp(Body.rotation, angle - Attraction, Lerp);
        }
    }

    private static Vector3 GetOppositCorner(bool planetInRightSide)
    {
        return planetInRightSide ? Planet.transform.position - Transform.position : Transform.position - Planet.transform.position;
    }

    private void OnDisable()
    {
        Message.HideMessage();
        InGame = false;
        Debug.Log($"{gameObject.name} disabled");
        
        OnShipDisabled?.Invoke();
    }

    private void OnEnable()
    {
        transform.position = new Vector3(0, -16, 0);
        Body.rotation = 0;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawLine(transform.position, transform.position + transform.up * 6);
    }
}
