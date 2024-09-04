using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float mainThrustForce;
    [SerializeField] float rotationThrustForce;
    Rigidbody rb;
    AudioSource AudioSource;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        AudioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        ProcessThrust();
        ProcessRotate();
    }
    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rb.AddRelativeForce(Vector3.up * mainThrustForce * Time.deltaTime);
            if (!AudioSource.isPlaying)
            {
                AudioSource.Play();
                AudioSource.loop = true;
            }
        }
        else
        {
            AudioSource.Pause();
        }
    }
    void ProcessRotate()
    {
        if (Input.GetKey(KeyCode.A))
        {
            ApplyRotation(rotationThrustForce);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            ApplyRotation(-rotationThrustForce);
        }
    }

    private void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false;
    }
}