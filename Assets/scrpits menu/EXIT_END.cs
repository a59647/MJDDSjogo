using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // Importar para usar Image

public class RetornarMenuAoClicar : MonoBehaviour, IPointerClickHandler
{
    [Tooltip("Nome da cena do menu para carregar.")]
    public string nomeDaCenaDoMenu = "MenuPrincipal";

    // Este método é chamado quando o objeto é clicado
    public void OnPointerClick(PointerEventData eventData)
    {
        // Carrega a cena do menu
        if (string.IsNullOrEmpty(nomeDaCenaDoMenu))
        {
            Debug.LogError("Nome da cena do menu não definido no Inspector!", this);
            return;
        }
        SceneManager.LoadScene(nomeDaCenaDoMenu);
    }

    void Awake()
    {
        //Verifica se o objeto tem os componentes necessários e os adiciona se não tiver.
        if (GetComponent<Collider2D>() == null)
        {
            gameObject.AddComponent<BoxCollider2D>(); // Adiciona um BoxCollider2D se não houver nenhum Collider2D
        }
        if (GetComponent<CanvasRenderer>() == null)
        {
            gameObject.AddComponent<CanvasRenderer>();
        }
        if (GetComponent<RectTransform>() == null)
        {
            gameObject.AddComponent<RectTransform>();
        }

        //Garante que o objeto interaja com eventos de UI.
        if (GetComponent<Image>() == null) // Alterado de Graphic para Image
        {
             Debug.LogWarning("Objeto não interage com eventos de UI,irá interagir apenas se possuir um Graphic");
        }
    }
}
