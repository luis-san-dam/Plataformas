using System;
using UnityEngine;

public class MovimientoEnemigo : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] private Rigidbody2D rb2D;
    [SerializeField] private Animator animator;
    [SerializeField] private EstadosEnemigo estadoActual;

    [Header("Movimiento Horizontal")]
    [SerializeField] private float velocidadMovimientoActual;
    [SerializeField] private float ultimaVelocidadDeMovimiento;
    [SerializeField] private Transform controladorPared;
    [SerializeField] private float distanciaRayoPared;
    [SerializeField] private LayerMask capasSuelo;
    private bool tocandoSueloPared;

    [Header("Esperar")]
    [SerializeField] private float tiempoAEsperar;
    private float tiempoAEsperarActual;

    private void Update()
    {
        tocandoSueloPared = Physics2D.Raycast(controladorPared.position, transform.right * -1, distanciaRayoPared, capasSuelo);

        if (tiempoAEsperarActual > 0)
        {
            tiempoAEsperarActual -= Time.deltaTime;
        }

        ControlarAnimaciones();
    }
    void FixedUpdate()
    {
        switch (estadoActual)
        {
            case EstadosEnemigo.Correr:
                ComportamientoCorrer();
                break;
            case EstadosEnemigo.Esperar:
                ComportamientoEsperar();
                break;
        }
        
    }

    private void CambiarAEstadoEsperar()
    {
        ultimaVelocidadDeMovimiento = velocidadMovimientoActual;
        velocidadMovimientoActual = 0;
        rb2D.linearVelocity = new Vector2(0, rb2D.linearVelocity.y);
        estadoActual = EstadosEnemigo.Esperar;
        tiempoAEsperarActual = tiempoAEsperar;
    }

    private void ComportamientoEsperar()
    {
        if(tiempoAEsperarActual < 0)
        {
            CambiarAEstadoCorrer();
        }
    }

    private void CambiarAEstadoCorrer()
    {
        velocidadMovimientoActual = ultimaVelocidadDeMovimiento * -1;
        estadoActual = EstadosEnemigo.Correr;
    }

    private void ComportamientoCorrer()
    {
        rb2D.linearVelocity = new Vector2(velocidadMovimientoActual, rb2D.linearVelocity.y);

        if (tocandoSueloPared)
        {
            Girar();
            CambiarAEstadoEsperar();
        }

        MirarEnLaDireccionDelMovimiento();
    }

    private void MirarEnLaDireccionDelMovimiento()
    {
        if ((velocidadMovimientoActual > 0 && !MirandoALaDerecha()) || (velocidadMovimientoActual < 0 && MirandoALaDerecha()))
        {
            Girar();
        }
    }

    private void Girar()
    {
        Vector3 rotacion = transform.eulerAngles;
        rotacion.y = rotacion.y == 0 ? 180:0;
        transform.eulerAngles = rotacion;
    }

    private bool MirandoALaDerecha()
    {
        return transform.eulerAngles.y == 180;
    }

    private void ControlarAnimaciones()
    {
        animator.SetFloat("VelocidadHorizontal", Mathf.Abs(rb2D.linearVelocity.x));
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(controladorPared.position, controladorPared.position + distanciaRayoPared * transform.right * -1 );
    }
}
