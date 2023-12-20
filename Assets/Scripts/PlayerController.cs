using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed = 1f;
    [SerializeField] float force = 1f;

    [SerializeField] float lineLength;
    [SerializeField] float offset;

    [SerializeField] bool isJumping = false;
    [SerializeField] bool showMap = false;

    [SerializeField] ParticleSystem jumpParticles;

    int coinsCollected = 0;
    [SerializeField] TextMeshProUGUI coinsCollectedText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(speed * Input.GetAxisRaw("Horizontal"), GetComponent<Rigidbody2D>().velocity.y);

        if (Input.GetAxisRaw("Horizontal") == 1) GetComponent<SpriteRenderer>().flipX = false;
        if (Input.GetAxisRaw("Horizontal") == -1) GetComponent<SpriteRenderer>().flipX = true;
        if (Input.GetButtonDown("Fire1") && !isJumping)
        {
            AudioManager.instance.PlaySFX("Jump");
            jumpParticles.Play();
            GetComponent<Rigidbody2D>().AddForce(Vector2.up * force, ForceMode2D.Impulse);
        }
        
        if (Input.GetKeyDown(KeyCode.M))
            if (!showMap) { SCManager.instance.LoadMap(); showMap = true; } 
            else { SCManager.instance.UnloadMap(); showMap = false; }

        Vector2 origin = new Vector2(transform.position.x, transform.position.y - offset);
        Vector2 target = new Vector2(transform.position.x, transform.position.y - offset - lineLength);
        Debug.DrawLine(origin, target, Color.black);

        RaycastHit2D raycast = Physics2D.Raycast(origin, Vector2.down, lineLength);

        if (raycast.collider == null)
        {
            isJumping = true;
            SetAnimation("jump");
        }
        else
        {
            isJumping = false;
            // Si está sobre una superficie pero se mueve lateralmente
            if (GetComponent<Rigidbody2D>().velocity.x != 0) SetAnimation("run");
            else SetAnimation("idle"); // Si está sobre una superficie pero no se mueve
        }
    }

    void SetAnimation(string name)
    {
        // Obtenemos todos los parámetros del Animator
        AnimatorControllerParameter[] parametros = GetComponent<Animator>().parameters;

        // Recorremos todos los parámetros y los ponemos a false
        foreach (var item in parametros) GetComponent<Animator>().SetBool(item.name, false);

        // Activamos el pasado por parámetro
        GetComponent<Animator>().SetBool(name, true);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision == null) return;
        if (collision.collider.CompareTag("Enemy")) {
            AudioManager.instance.PlaySFX("Hit");
            AudioManager.instance.PlayMusic("LoseALife");
            SCManager.instance.LoadScene("GameOver");
        }
        else if (collision.collider.CompareTag("Coin"))
        {
            Destroy(collision.gameObject);
            AudioManager.instance.PlaySFX("CollectCoin");
            coinsCollected++;
            coinsCollectedText.text = "Coins: " + coinsCollected;
        }
    }
}
