using UnityEngine;


public class Player : MonoBehaviour
{
    [SerializeField] private Vector3 direction;
    [SerializeField] private float gravity = -9.8f;
    [SerializeField] private float force = 5.0f;
    [SerializeField] Sprite[] sprites;
    private int spriteIndex;
    private SpriteRenderer playerSprite;
    void Awake()
    {
        playerSprite = GetComponent<SpriteRenderer>();
    }
    private void Start()
    {
        spriteIndex = 0;
        InvokeRepeating(nameof(AnimateSprite), 0.015f, 0.015f);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            AudioManager.instance.PlayAudio(Sound.Jump);
            direction = Vector3.up * force;
        }

        direction.y += gravity * Time.deltaTime;
        transform.position += direction * Time.deltaTime;
    }

    void AnimateSprite()
    {
        spriteIndex++;
        if(spriteIndex >= sprites.Length)
        {
            spriteIndex = 0;
        }
        playerSprite.sprite = sprites[spriteIndex];

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null) { 
            if(collision.gameObject.tag == "Pipes" || collision.gameObject.tag == "Ground")
            {
                AudioManager.instance.PlayAudio(Sound.Lose);
                GameManager.instance.GameOver();

            }
            else if (collision.gameObject.tag == "Scorer")
            {
                AudioManager.instance.PlayAudio(Sound.Scoring);
                GameManager.instance.IncreaseScore();
            }
        }
    }
}
