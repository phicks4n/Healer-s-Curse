using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Inventory.Model;

[System.Serializable]
public class GameData
{
    public Vector2 playerPosition;
    public int sceneIndex;
    public int initialSceneIndex;
    public int enemyType;
    public List<InventoryItem> inventoryData;
    public List<InventoryItem> equipmentData;
    public InventoryItem headSlot, armorSlot, glovesSlot, bootsSlot, mainHandSlot, offHandSlot, ringSlot, necklaceSlot;
    public int armor, attack, crit, dodge, energy, health, resist, magic, mana, speed;
    public int currentHealth;

    // the values defined in this constructor will be the default values
    // the game starts with when there's no data to load
    public GameData() 
    {   
        playerPosition = Vector2.zero;
        sceneIndex = 0;
        initialSceneIndex = 0;
        enemyType = 0;
        inventoryData = new List<InventoryItem>();
        equipmentData = new List<InventoryItem>();
        headSlot = new InventoryItem();
        armorSlot = new InventoryItem();
        glovesSlot = new InventoryItem();
        bootsSlot = new InventoryItem();
        mainHandSlot = new InventoryItem();
        offHandSlot = new InventoryItem();
        ringSlot = new InventoryItem();
        necklaceSlot = new InventoryItem();
        armor = 20;
        attack = 15;
        crit = 2;
        dodge = 3;
        energy = 50;
        health = 90;
        resist = 0;
        magic = 0;
        mana = 10;
        speed = 20;
        currentHealth = 90;
    }
}