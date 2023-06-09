using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//커스템 메뉴를 생성하는 속성
[CreateAssetMenu(fileName = "Item", menuName = "Scriptable Object/ItemData")]
public class ItemData : ScriptableObject
{
    public enum ItemType { Melee, Range, Glove, Shoe, Heal }

    [Header("# Main Info")]
    public ItemType itemType;
    public int itemId;
    public string itemName;
    [TextArea]//text영역을 크게(inspector)
    public string itemDesc;
    public Sprite itemIcon;

    [Header("# Level Data")]
    public float baseDamage;
    public int baseCount;//관통

    //레벨업
    public float[] damages;
    public int[] counts;

    [Header("# Weapon")]
    public GameObject projectile;
    public Sprite hand;
}
