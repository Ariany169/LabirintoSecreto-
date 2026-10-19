using UnityEngine;

public class Coletavel : MonoBehaviour
{
    public void Coletar()
    {
        Debug.Log($"Coletando {gameObject.name}");

        Destroy(this.gameObject);
    }
}