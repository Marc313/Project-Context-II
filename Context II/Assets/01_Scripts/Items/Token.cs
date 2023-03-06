using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Token")]
public class Token : Item
{
    public enum Side { CEO = 0, Citizen = 1}

    public string description;
    public Color color;
    public Side side = Side.Citizen;
}
