using UnityEngine;
using UnityEngine.InputSystem;

public class RaioInteracao : MonoBehaviour
{
    private Renderer objetoAtual;
    private Color corOriginal;

    private void Update()
    {
        RaycastHit hit;

        if (Physics.Raycast(
            transform.position,
            transform.TransformDirection(Vector3.forward),
            out hit,
            Mathf.Infinity))
        {
            // Raio amarelo quando está atingindo um objeto
            Debug.DrawRay(
                transform.position,
                transform.TransformDirection(Vector3.forward) * hit.distance,
                Color.yellow
            );

            Renderer renderer = hit.collider.GetComponent<Renderer>();

            // Se encontrou um objeto com Renderer
            if (renderer != null)
            {
                // Se for um objeto diferente do anterior
                if (objetoAtual != renderer)
                {
                    // Devolve a cor do objeto anterior
                    if (objetoAtual != null)
                    {
                        objetoAtual.material.color = corOriginal;
                    }

                    // Guarda o novo objeto
                    objetoAtual = renderer;
                    corOriginal = renderer.material.color;

                    // Muda para vermelho
                    renderer.material.color = Color.red;
                }
            }

            // Guarda o objeto atingido
            GameObject obj = hit.collider.gameObject;

            // Coleta somente quando apertar F
            if (obj.CompareTag("coletavel") &&
                Keyboard.current.fKey.wasPressedThisFrame)
            {
                Coletavel obj_col = obj.GetComponent<Coletavel>();

                if (obj_col != null)
                {
                    obj_col.Coletar();
                }
            }
        }
        else
        {
            // Raio azul quando não está atingindo nada
            Debug.DrawRay(
                transform.position,
                transform.TransformDirection(Vector3.forward) * 1000,
                Color.blue
            );

            // Quando o raio sai do objeto, volta para a cor original
            if (objetoAtual != null)
            {
                objetoAtual.material.color = corOriginal;
                objetoAtual = null;
            }
        }
    }
}