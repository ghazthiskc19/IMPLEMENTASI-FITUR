using UnityEngine;
using UnityEngine.Tilemaps;

public class PowerUpSpawner : MonoBehaviour
{
    public Tilemap tilemapGround;
    public Tilemap tilemapWall;
    public float spawnDuration;
    [SerializeField] private float timer;
    public GameObject[] TankPowerUp;
    public GameObject[] WeaponPowerUp;
    private bool firstSpawn = false;

    void Update()
    {
        if (!firstSpawn)
        {
            SpawnPowerUp();
            firstSpawn = true;
            timer = 0f;
        }


        timer += Time.deltaTime;
        if (timer > spawnDuration)
        {
            SpawnPowerUp();
            timer = 0f;
        }
    }

    void SpawnPowerUp()
    {
        RemoveAllPowerUp();
        Vector3 tankPowerUpPos = GetRandomPosition();
        Vector3 weaponPowerUpPos = GetRandomPosition();
        GameObject tankPowerUpObj = Instantiate(GetRandomPowerUps(PowerUpsCategory.tank), tankPowerUpPos, Quaternion.identity, tilemapGround.transform);
        GameObject weaponPowerUpObj = Instantiate(GetRandomPowerUps(PowerUpsCategory.weapon), weaponPowerUpPos, Quaternion.identity, tilemapGround.transform);
    }

    Vector3 GetRandomPosition()
    {
        tilemapGround.CompressBounds();
        BoundsInt boundsInt = tilemapGround.cellBounds;
        Vector3Int randomCell;
        do
        {
            int randX = Random.Range(boundsInt.xMin, boundsInt.xMax);
            int randY = Random.Range(boundsInt.yMin, boundsInt.yMax);
            randomCell = new Vector3Int(randX, randY, 0);
        }
        while (tilemapWall.HasTile(randomCell));
        
        return tilemapGround.GetCellCenterWorld(randomCell);
    }

    GameObject GetRandomPowerUps(PowerUpsCategory powerUpsCategory)
    {
        if (powerUpsCategory == PowerUpsCategory.tank)
        {
            int random = Random.Range(0, TankPowerUp.Length);
            return TankPowerUp[random];
        }
        else if (powerUpsCategory == PowerUpsCategory.weapon)
        {
            int random = Random.Range(0, WeaponPowerUp.Length);
            return WeaponPowerUp[random];
        }
        return null;
    }

    void RemoveAllPowerUp()
    {
        GameObject[] powerUps = GameObject.FindGameObjectsWithTag("PowerUp");
        foreach (GameObject powerUp in powerUps)
        {
            Destroy(powerUp);
        }
    }
}
