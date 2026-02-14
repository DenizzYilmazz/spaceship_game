using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Lander : MonoBehaviour
{
    
    public static Lander Instance { get; private set; }
    
    public event EventHandler OnUpForce;
    public event EventHandler OnRightForce;
    public event EventHandler OnLeftForce;
    public event EventHandler OnBefornForce;
    public event EventHandler OnCoinPickUp;
    public event EventHandler <OnLandedEventArgs> OnLanded;
    public class OnLandedEventArgs: EventArgs
    {
        public int score;
    }



    private Rigidbody2D landerRigidbody2D;
    private float fuelAmount;
    private float fuelAmountMax = 10f;

    private void Awake()
    {
        Instance = this;
        fuelAmount = fuelAmountMax = 10f;
        landerRigidbody2D = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        OnBefornForce?.Invoke(this, EventArgs.Empty);

        Debug.Log(fuelAmount);
        if(fuelAmount <= 0f)
        {
            return;
        }
        if(Keyboard.current.upArrowKey.isPressed ||
           Keyboard.current.leftArrowKey.isPressed ||
           Keyboard.current.rightArrowKey.isPressed)
        {
            ConsumeFuel();
        }
        
        if (Keyboard.current.upArrowKey.isPressed)
        {
            float force = 700f;
            landerRigidbody2D.AddForce(force * transform.up * Time.deltaTime);
            OnUpForce?.Invoke(this, EventArgs.Empty);
        }

        if (Keyboard.current.leftArrowKey.isPressed)
        {
            float turnSpeed = +100f;
            landerRigidbody2D.AddTorque(turnSpeed * Time.deltaTime);
            OnLeftForce?.Invoke(this, EventArgs.Empty);
        }

        if (Keyboard.current.rightArrowKey.isPressed)
        {
            float turnSpeed = -100f;
            landerRigidbody2D.AddTorque(turnSpeed * Time.deltaTime);
            OnRightForce?.Invoke(this, EventArgs.Empty);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision2D)
    {
        if (!collision2D.gameObject.TryGetComponent(out LandingPad landingPad))
        {
            Debug.Log("Crashed on the Terrain!");
            return;
        }

        float SoftLandingVelocityMagnitude = 4f;
        float relativeVelocityMagnitude = collision2D.relativeVelocity.magnitude;
        if (relativeVelocityMagnitude > SoftLandingVelocityMagnitude)
        {
            Debug.Log("Landed too hard!");
            return;
        }
        Debug.Log("Successful landing");

        float dotVector = Vector2.Dot(Vector2.up, transform.up);
        float minDotVector = .90f;
        if (dotVector < minDotVector)
        {
            Debug.Log("Landed on a too steep angle!");
            return;
        }
        Debug.Log("Successful Landing!");

        float maxScoreAmountLandingAngle = 100;
        float scoreDotVectorMultiplier = 10f;
        float landingAngleScore = maxScoreAmountLandingAngle - Mathf.Abs(dotVector - 1) * scoreDotVectorMultiplier * maxScoreAmountLandingAngle;

        float maxScoreAmountLandingSpeed = 100f;
        float landingspeedScore = (SoftLandingVelocityMagnitude - relativeVelocityMagnitude) * maxScoreAmountLandingSpeed;

        Debug.Log("landingAgnleScore: " + landingAngleScore);
        Debug.Log("landingspeedScore: " + landingspeedScore);

        int score = Mathf.RoundToInt((landingAngleScore + landingspeedScore) * landingPad.GetScoreMultiplier());

        Debug.Log("score: " +  score);
        OnLanded?.Invoke(this, new OnLandedEventArgs
        {
            score = score,
        });
    }

    private void OnTriggerEnter2D(Collider2D collision2D)
    {
        if (collision2D.gameObject.TryGetComponent(out FuelPickUp fuelPickUp))
        {
            float addFuelAmount = 10f;
            fuelAmount += addFuelAmount;
            if (fuelAmount > fuelAmountMax)
            {
                fuelAmount = fuelAmountMax;
            }
            fuelPickUp.DestroySelf();
        }
        if (collision2D.gameObject.TryGetComponent(out CoinPickUp coinPickUp))
        {
            OnCoinPickUp?.Invoke(this, EventArgs.Empty);
            coinPickUp.DestroySelf();
        }
    }
    private void ConsumeFuel()
    {
        float fuelConsumptionAmount = 1f;
        fuelAmount -= fuelConsumptionAmount * Time.deltaTime;
    }

    public float GetFuel()
    {
        return fuelAmount;
    }

    public float GetFuelAmountNormalized()
    {
        return fuelAmount / fuelAmountMax;
    }
    public float GetSpeedX()
    {
        return landerRigidbody2D.linearVelocityX;
    }

    public float GetSpeedY()
    {
        return landerRigidbody2D.linearVelocityY;
    }
}

