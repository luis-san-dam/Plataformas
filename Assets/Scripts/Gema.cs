using System;
using UnityEngine;

public class Gema : MonoBehaviour
{
    public static Action GemaRecolectada;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Recolectar();
        }
    }

    private void Recolectar()
    {
        GemaRecolectada?.Invoke();
        Destroy(gameObject);
    }
}
