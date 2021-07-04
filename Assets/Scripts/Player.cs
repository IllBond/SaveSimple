using System.Collections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    public int level;
    public int health;

    public void LoadData(SaveSystem.Save.PlayerSaveData save) {
        transform.position = new Vector3(save.Position.x, save.Position.y, save.Position.z);
        level = save.level;
        health= save.health;

    }
}
