using UnityEngine;


public class Enemy : MonoBehaviour
{
    public void LoadData(SaveSystem.Save.EnemySaveData save) {
        transform.position = new Vector3(save.Position.x, save.Position.y, save.Position.z);

    }
}
