using UnityEngine;

using UnityEngine.InputSystem;


public class Movement : MonoBehaviour
{
    
    [SerializeField] InputAction thrust;
    [SerializeField] InputAction rotation;
    [SerializeField] float thrustPower = 5;
    [SerializeField] float rotationPower = 100;

    [SerializeField] AudioClip Engine;
    

    bool a_play; 
    bool a_change; 
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
            rb.AddRelativeForce(Vector3.up * thrustPower *Time.fixedDeltaTime);
            if(!audioSource.isPlaying){
                audioSource.PlayOneShot(Engine);
            }
        }
        else{
                audioSource.Stop();
            }
    }
    private void Rotation()
    {
        float rotationInput = rotation.ReadValue<float>();
        if(rotationInput < 0){
            ApplyRotation(rotationPower);
        }
        else if(rotationInput > 0){
            ApplyRotation(-rotationPower);
        }
    }

    private void ApplyRotation(float rotationFrame){
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotationFrame * Time.fixedDeltaTime);
        rb.freezeRotation = false;
    }
}
