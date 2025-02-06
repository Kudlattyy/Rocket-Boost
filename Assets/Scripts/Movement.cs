using UnityEngine;

using UnityEngine.InputSystem;


public class Movement : MonoBehaviour
{
    
    [SerializeField] InputAction thrust;
    [SerializeField] InputAction rotation;
    [SerializeField] float thrustPower = 5;
    [SerializeField] float rotationPower = 100;

    [SerializeField] ParticleSystem LeftBoosterParticles;
    [SerializeField] ParticleSystem RightBoosterParticles;
    [SerializeField] ParticleSystem MainBoosterParticles;
    [SerializeField] AudioClip Engine;
    

    Rigidbody rb;
    AudioSource audioSource;


    void Start() 
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    private void FixedUpdate() 
    {
        Thrust();
        Rotation();
    }
    private void OnEnable() 
    {
        thrust.Enable();
        rotation.Enable();
    }

    private void Thrust(){
        if(thrust.IsPressed())
        {
            StartThrust();
        }
        else
        {
            StopThrusting();
        }
    }
    private void StartThrust()
    {
        rb.AddRelativeForce(Vector3.up * thrustPower * Time.fixedDeltaTime);
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(Engine);
        }
        if (!MainBoosterParticles.isPlaying)
        {
            MainBoosterParticles.Play();
        }
    }

    private void StopThrusting()
    {
        audioSource.Stop();
        MainBoosterParticles.Stop();
    }


    private void Rotation()
    {
        float rotationInput = rotation.ReadValue<float>();
        if(rotationInput < 0)
        {
            RightRotation();
        }
        else if(rotationInput > 0)
        {
            LeftRotation();
        }
        else
        {
            StopRotation();
        }
    }

    private void RightRotation()
    {
        ApplyRotation(rotationPower);
        if (!RightBoosterParticles.isPlaying)
        {
            LeftBoosterParticles.Stop();
            RightBoosterParticles.Play();
        }
    }
    private void LeftRotation()
    {
        ApplyRotation(-rotationPower);
        if (!LeftBoosterParticles.isPlaying)
        {
            RightBoosterParticles.Stop();
            LeftBoosterParticles.Play();
        }
    }

    private void StopRotation()
    {
        RightBoosterParticles.Stop();
        LeftBoosterParticles.Stop();
    }

    private void ApplyRotation(float rotationFrame){
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotationFrame * Time.fixedDeltaTime);
        rb.freezeRotation = false;
    }
}
