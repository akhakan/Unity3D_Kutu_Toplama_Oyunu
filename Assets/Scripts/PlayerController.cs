using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player Settings")]

    [SerializeField] private Transform _orientationTransform;

    private Rigidbody _playerRigidbody;

    private float _verticalInput, _horizontalInput;

    private Vector3 _movementDirection;

    [SerializeField] private float _movementSpeed = 10f;


    [Header("Effect Settings")]

    [SerializeField] private GameObject explosionPrefab;


    [Header("Audio Settings")]

    [SerializeField] private AudioClip hitSoundRed;
    [SerializeField] private AudioClip hitSoundBlue;
    [SerializeField] private AudioClip hitSoundGreen;
    [SerializeField] private AudioClip hitSoundYellow;
    [SerializeField] private AudioClip hitSoundMagenta;

    private AudioSource audioSource;



    private void Awake()
    {
        _playerRigidbody = GetComponent<Rigidbody>();
        _playerRigidbody.freezeRotation = true;

        // Main Camera'dan AudioSource al
        audioSource = Camera.main.GetComponent<AudioSource>();
    }

    private void Update()
    {
        _verticalInput = Input.GetAxisRaw("Vertical");
        _horizontalInput = Input.GetAxisRaw("Horizontal");
        _movementDirection = _orientationTransform.forward * _verticalInput + _orientationTransform.right * _horizontalInput;
    }


    private void FixedUpdate()
    {

        _playerRigidbody.AddForce(_movementDirection.normalized * _movementSpeed, ForceMode.Force);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Collectible"))
        {

            CollectibleBox box = other.GetComponent<CollectibleBox>();
            if (box != null)
            {
                ScoreManager.instance.AddScore(box.ScoreValue);

                // Patlama ve ses efekti(isteğe bağlı)
                Explode(box, other);

                // 🗑 YOK ET
                Destroy(other.gameObject);
            }
        }
    }


    void Explode(CollectibleBox box,Collider other)
    {
        // 🎨 Kutunun rengini al
        Renderer rend = other.GetComponent<Renderer>();
        Color boxColor = rend.material.color;
        boxColor *= Random.Range(0.85f, 1.15f);
        
        // 🔊🎧 Renge göre ses seç
        AudioClip selectedSound = GetSoundByType(box);
        audioSource.PlayOneShot(selectedSound);

        // 💥 PATLAMA
        GameObject explosion = Instantiate(explosionPrefab, other.transform.position, Quaternion.identity);

        ParticleSystem ps = explosion.GetComponent<ParticleSystem>();
        var main = ps.main;
        main.startColor = boxColor;
    }

    AudioClip GetSoundByType(CollectibleBox box)
    {
        switch (box.BoxType)
        {
            case BoxType.Red: return hitSoundRed;
            case BoxType.Blue: return hitSoundBlue;
            case BoxType.Green: return hitSoundGreen;
            case BoxType.Yellow: return hitSoundYellow;
            case BoxType.Magenta: return hitSoundMagenta;
        }
        return hitSoundRed;
    }

}

