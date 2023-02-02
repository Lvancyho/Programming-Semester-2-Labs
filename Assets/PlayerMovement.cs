using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float movementSpeed = 10f;
    [SerializeField] private float rotationalSpeed = 75f;
    [SerializeField] private float jumpVelocity = 10f;

    [Header("Shooting")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform bulletParent;
    [SerializeField] private float bulletVelocity = 40f;

    [Header("Checks")]
    [SerializeField] private float distanceToGround = 1.25f;
    [SerializeField] private LayerMask groundLayer;

    private float vInput;
    private float hInput;
    private Rigidbody rb;
    private CapsuleCollider capsuleCollider;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();
    }

    private void Update()
    {
        MovePlayer();
        Jump();
        Shoot();
    }

    private void MovePlayer()
    {
        vInput = Input.GetAxis("Vertical") * movementSpeed;
        hInput = Input.GetAxis("Horizontal") * rotationalSpeed;

        transform.Translate(Vector3.forward * vInput * Time.deltaTime);
        transform.Rotate(Vector3.up * hInput * Time.deltaTime);
    }

    private void Jump()
    {
        bool isPlayerGrounded = Physics.Raycast(transform.position, Vector3.down, distanceToGround, groundLayer);

        if (isPlayerGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * jumpVelocity, ForceMode.Impulse);
        }
    }

    private void Shoot()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);

            bullet.transform.parent = bulletParent;
            bullet.GetComponent<Rigidbody>().velocity = transform.forward * bulletVelocity;
            Destroy(bullet.gameObject, 5f);
        }
    }
}