using UnityEngine;
using UnityEngine.UI; // Necesario para modificar la barra
using UnityEngine.SceneManagement; // Necesario para reiniciar el nivel

public class SistemaEnergia : MonoBehaviour
{
    public float energiaMaxima = 100f;
    public float energiaActual;
    public float desgastePorSegundo = 5f; // Cuánta energía pierde cada segundo
    public Slider barraUI;

    void Start()
    {
        energiaActual = energiaMaxima;

        if (barraUI != null)
        {
            barraUI.maxValue = energiaMaxima;
            barraUI.value = energiaActual;
        }
    }

    void Update()
    {
        // Restar energía gradualmente con el tiempo
        energiaActual -= desgastePorSegundo * Time.deltaTime;

        if (barraUI != null)
        {
            barraUI.value = energiaActual;
        }

        // Si la energía llega a 0, reiniciamos el nivel (Muere)
        if (energiaActual <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    // Detectar si tocamos una batería o un obstáculo
    private void OnTriggerEnter2D(Collider2D colision)
    {
        if (colision.gameObject.CompareTag("Bateria"))
        {
            energiaActual += 20f; // Recupera 20 de energía
            if (energiaActual > energiaMaxima) energiaActual = energiaMaxima; // No pasar de 100

            Destroy(colision.gameObject); // Desaparecer la batería de la pantalla
        }
        else if (colision.gameObject.CompareTag("Obstaculo"))
        {
            energiaActual -= 30f; // Quitar golpe fuerte de energía al tocar pinchos/ácido
        }
    }
}