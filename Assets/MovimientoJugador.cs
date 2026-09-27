using UnityEngine;

public class MovimientoJugador : MonoBehaviour
{
    public float velocidad = 5f;
    public float fuerzaSalto = 7f;
    private Rigidbody2D rb;
    private bool enElSuelo = true;

    void Start()
    {
        // Busca el componente de f�sicas que le pusimos al jugador
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Movimiento izquierda/derecha usando las flechas o A/D
        float movimiento = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(movimiento * velocidad, rb.linearVelocity.y);

        // Saltar al presionar la barra espaciadora
        if (Input.GetButtonDown("Jump") && enElSuelo)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, fuerzaSalto);
            enElSuelo = false; // Ya no estamos en el suelo porque saltamos
        }
    }

    // Verifica si chocamos con el piso para poder volver a saltar
    private void OnCollisionEnter2D(Collision2D colision)
    {
        if (colision.gameObject.CompareTag("Suelo"))
        {
            enElSuelo = true;
        }
    }
}