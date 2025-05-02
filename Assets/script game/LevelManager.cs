using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }

    [Header("Target Objects")]
    public GameObject[] targetObjects;

    [Header("Projectile Settings")]
    public int maxProjectiles = 5;
    private int projectilesLaunched = 0;

    [Header("Result Assets")]
    public GameObject zeroHitsAsset;
    public GameObject oneHitAsset;
    public GameObject twoHitsAsset;
    public GameObject threeHitsAsset;

    private int spritesChangedCount = 0;
    private bool levelEnded = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        if (targetObjects == null || targetObjects.Length != 3)
        {
            Debug.LogError("Por favor, atribua exatamente 3 objetos alvo no Inspector.");
            enabled = false;
            return;
        }

        // Garante que os result assets estejam desativados no início
        SetActiveResultAssets(false);
    }

    public void IncrementProjectilesLaunched()
    {
        if (levelEnded) return; // Impede contar projéteis após o fim do nível

        projectilesLaunched++;
        Debug.Log("Projéteis Lançados: " + projectilesLaunched);

        // Verifica se o número máximo de projéteis foi atingido
        if (projectilesLaunched >= maxProjectiles)
        {
            EndLevel(); // Encerra o nível se o máximo de projéteis for atingido
        }
    }

    // Este método é chamado pelo TargetBehavior quando o sprite de um alvo é alterado.
    public void TargetSpriteChanged()
    {
        if (levelEnded) return; // Impede contar mudanças de sprite após o fim do nível

        spritesChangedCount++;
        Debug.Log("Sprites Alterados: " + spritesChangedCount);

        // Não encerra o nível aqui, mas você pode adicionar lógica extra se precisar.
        if (spritesChangedCount >= targetObjects.Length)
        {
            EndLevel();
        }
    }

    private void EndLevel()
    {
        levelEnded = true;
        ShowResultAsset();
        // Desativa a capacidade de atirar
        if (Reticle.Instance != null)
        {
            Reticle.Instance.enabled = false;
            Reticle.Instance.gameObject.SetActive(false);
        }
    }

    private void ShowResultAsset()
    {
        SetActiveResultAssets(false); // Desativa todos os assets primeiro

        if (spritesChangedCount == 0)
        {
            if (zeroHitsAsset != null) zeroHitsAsset.SetActive(true);
        }
        else if (spritesChangedCount == 1)
        {
            if (oneHitAsset != null) oneHitAsset.SetActive(true);
        }
        else if (spritesChangedCount == 2)
        {
            if (twoHitsAsset != null) twoHitsAsset.SetActive(true);
        }
        else if (spritesChangedCount == targetObjects.Length) //mostra o asset de 3 acertos
        {
            if (threeHitsAsset != null) threeHitsAsset.SetActive(true);
        }
    }

    private void SetActiveResultAssets(bool active)
    {
        if (zeroHitsAsset != null) zeroHitsAsset.SetActive(active);
        if (oneHitAsset != null) oneHitAsset.SetActive(active);
        if (twoHitsAsset != null) twoHitsAsset.SetActive(active);
        if (threeHitsAsset != null) threeHitsAsset.SetActive(active);
    }
}
