using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SCManager : MonoBehaviour
{
    // Creamos una variable estática para almacenar la única instancia
    public static SCManager instance;

    private void Awake()
    {
        // Garantizamos que solo exista una instancia del SCManager
        // Si no hay instancias previas se asigna la actual
        // Si hay instancias se destruye la nueva
        if (instance == null) instance = this;
        else { Destroy(gameObject); return; }

        // No destruimos el SceneManager aunque se cambie de escena
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Método para cargar una nueva escena por nombre
    public void LoadScene(string sceneName) => SceneManager.LoadScene(sceneName); //LoadSceneMode.Additive para cargar una escena sobre otra ||| EJEMPLO: Mostrar el mapa mientras sigue el juego corriendo

    public void LoadMap() => SceneManager.LoadScene("WorldMap", LoadSceneMode.Additive);
    public void UnloadMap() => SceneManager.UnloadSceneAsync("WorldMap");
}
