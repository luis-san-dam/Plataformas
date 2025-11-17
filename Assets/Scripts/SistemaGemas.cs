using System;
using TMPro;
using UnityEngine;

public class SistemaGemas : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textoCantidadGemas;
    [SerializeField] private int cantidadGemas;
    [SerializeField] private int cantidadMaximaDeGemas;

    private void Start()
    {
        ActualizarTexto();
    }

    private void OnEnable()
    {
        Gema.GemaRecolectada += SumarGemas;
    }

    void OnDisable()
    {
        Gema.GemaRecolectada -= SumarGemas;
    }

    private void SumarGemas()
    {
        if (cantidadGemas + 1 > cantidadMaximaDeGemas) { return; }
        cantidadGemas += 1;
        ActualizarTexto();
    }

    private void ActualizarTexto()
    {
        textoCantidadGemas.text = cantidadGemas.ToString("D2");
    }
}