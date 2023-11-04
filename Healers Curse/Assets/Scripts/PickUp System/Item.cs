using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Inventory.Model;

public class Item : MonoBehaviour
{
    [field: SerializeField]
    public ItemSO InventoryItem { get; private set; }
    [field: SerializeField]
    public int Quantity { get; set; } = 1;
    [field: SerializeField]
    private AudioSource audioSource;
    [field: SerializeField]
    private float duration = 0.3f;

    private void Start()
    {
        GetComponent<SpriteRenderer>().sprite = InventoryItem.ItemImage;
    }

    internal void DestroyItem()
    {
        GetComponent<Collider2D>().enabled = false;
        StartCoroutine(AnimateItemPickup());
    }

    private IEnumerator AnimateItemPickup()
    {
        //audioSource.Play();
        Vector3 startScale = transform.localScale;
        Vector3 endScale = Vector3.zero;
        float currentTime = 0;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            transform.localScale = Vector3.Lerp(startScale, endScale, currentTime / duration);
            yield return null;
        }
        transform.localScale = endScale;
        Destroy(gameObject);
    }
}
