using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    protected List<Item> _items = new List<Item>();
    [SerializeField] protected Image[] _images;

    protected void Awake()
    {
        foreach (var image in _images)
        {
            image.gameObject.SetActive(false);
        }
    }

    #region Test
    // public List<Item> Items;  
    // protected void Update()
    // {
    //     if (Input.GetKeyDown(KeyCode.E))
    //     {
    //         Item item = Items[Random.Range(0, Items.Count)];
    //         AddItem(item);
    //         Items.Remove(item);
    //     }

    //     if(Input.GetKeyDown(KeyCode.X))
    //         Debug.Log($"Is item deleted? {DelItem(_items[Random.Range(0, _items.Count)])}");

    //     if(Input.GetKeyDown(KeyCode.F))
    //         Debug.Log($"Is item exists? {HasItem(_items[Random.Range(0, _items.Count)])}");            
    // }
    #endregion

    /// <summary>
    /// Add item into inventory.
    /// Turn off game object and show item's icon on canvas 
    /// </summary>
    /// <param name="item"></param>
    public void AddItem(Item item)
    {
        _items.Add(item);
        int index = _items.IndexOf(item);

        _images[index].sprite = _items[index].Icon;
        _images[index].gameObject.SetActive(true);
        item.gameObject.SetActive(false);
        Debug.Log(item.name + " ");
    }

    /// <summary>
    /// Delete item from inventory and lift up icons
    /// </summary>
    /// <param name="item"></param>
    /// <returns>True if item exists and was deleted else false</returns>
    public bool DelItem(Item item)
    {
        if (_items.Remove(item))
        {
            LiftImage();
            return true;
        }

        return false;
    }

    /// <summary>
    /// Check has inventory got the item
    /// </summary>
    /// <param name="item"></param>
    /// <returns>True if item is in inventory else false</returns>
    public bool HasItem(Item item) => _items.Exists((i) => item == i);

    protected void LiftImage()
    {
        for (int i = 0; i < _images.Length; i++)
        {
            if (i < _items.Count)
                _images[i].sprite = _items[i].Icon;
            else
            {
                _images[i].sprite = null;
                _images[i].gameObject.SetActive(false);
            }
        }
    }
}
