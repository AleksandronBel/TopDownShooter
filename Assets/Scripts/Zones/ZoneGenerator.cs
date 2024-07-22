using UnityEngine;

public class ZoneGenerator : MonoBehaviour
{
    [SerializeField] GameObject _slowZonePrefab;
    [SerializeField] GameObject _deathZonePrefab;
    [SerializeField] int _slowZoneCount = 3;
    [SerializeField] int _deathZoneCount = 2;
    [SerializeField] float _mapWidth = 40f;
    [SerializeField] float _mapHeight = 30f;

    [SerializeField] float _minDistanceBetweenZones = 3f;

    void Start()
    {
        GenerateZones(_slowZonePrefab, _slowZoneCount, 3f);
        GenerateZones(_deathZonePrefab, _deathZoneCount, 1f);
    }

    void GenerateZones(GameObject zonePrefab, int zoneCount, float radius)
    {
        for (int i = 0; i < zoneCount; i++)
        {
            Vector3 position;
            bool allowablePosition;
            do
            {
                position = new Vector3
                    (
                    Random.Range(-_mapWidth/2 + radius + _minDistanceBetweenZones, _mapWidth/2 - radius - _minDistanceBetweenZones),
                    0,
                    Random.Range(-_mapHeight/2 + radius + _minDistanceBetweenZones, _mapHeight/2 - radius - _minDistanceBetweenZones)
                    );

                allowablePosition = true;

                foreach (Transform zone in transform)
                {
                    if (Vector3.Distance(position, zone.position) < radius + zone.localScale.x/2 + _minDistanceBetweenZones)
                    {
                        allowablePosition = false;
                        break;
                    }
                }
            } while (!allowablePosition);

            GameObject newZone = Instantiate(zonePrefab, position, Quaternion.identity);
            newZone.transform.parent = transform;
            newZone.transform.localScale = new Vector3(radius * 2, radius * 2, radius * 2); 
        }
    }
}

