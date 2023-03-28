using UnityEngine;

public class Item : PhysicalObject
{
    public Sprite Icon;
    private Inventory _inventory;

    protected override void Awake()
    {
        base.Awake();
        _inventory = FindObjectOfType<Inventory>();
    }
    public override void Interact()
    {
        _inventory.AddItem(GetComponent<Item>());
    }
}
