using JetBrains.Annotations;
using System.Collections.Generic;

public class Inventory
{
    public List<Item> items;

    public Inventory()
    {
        items = new List<Item>();
    }

    public bool ContainsItems(Item _item)
    {
        return items.Contains(_item);
    }
}
