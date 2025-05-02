using UnityEngine;

public class TargetBehavior : MonoBehaviour
{
    public Sprite changedSprite; // Assign the new sprite in the Inspector
    private SpriteRenderer sr;
    private bool spriteChanged = false;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        if (sr == null)
        {
            Debug.LogError("TargetBehavior needs a SpriteRenderer component on the same GameObject.", this);
            enabled = false; // Disable this script if no SpriteRenderer
            return;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Projectile") && !spriteChanged)
        {
            ChangeSprite();
            spriteChanged = true; // Impede múltiplas mudanças de sprite
            Destroy(collision.gameObject); // Destroy the projectile
        }
    }

     private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Projectile") && !spriteChanged)
        {
            ChangeSprite();
            spriteChanged = true; // Impede múltiplas mudanças de sprite
            Destroy(other.gameObject);
        }
    }

    void ChangeSprite()
    {
        if (changedSprite != null && sr != null)
        {
            sr.sprite = changedSprite;
            LevelManager.Instance.TargetSpriteChanged(); // Notify the LevelManager
        }
    }
}