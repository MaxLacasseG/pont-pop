using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehiculeSpawner : MonoBehaviour {
    [SerializeField]
    public List<Vector2> spawnPoints;
    [SerializeField]
    public List<GameObject> vehiculePrefabs;

    public void SpawnVehicule () {
        int bonus = GameMngr.instance.bonus;
        int pointIndex = Random.Range (0, 2);
        int vehiculeIndex = 0;
        GameObject v = null;
        switch (bonus) {
            case (1):
                vehiculeIndex = Random.Range (0, 2);
                v = Instantiate (vehiculePrefabs[vehiculeIndex], spawnPoints[pointIndex], Quaternion.identity, null);
                v.GetComponent<Vehicule> ().direction = pointIndex == 0 ? 1 : -1;
                break;
            case (2):
                vehiculeIndex = Random.Range (1, 3);
                v = Instantiate (vehiculePrefabs[vehiculeIndex], spawnPoints[pointIndex], Quaternion.identity, null);
                v.GetComponent<Vehicule> ().direction = pointIndex == 0 ? 1 : -1;

                break;
            case (3):
                v = Instantiate (vehiculePrefabs[2], spawnPoints[pointIndex], Quaternion.identity, null);
                v.GetComponent<Vehicule> ().direction = pointIndex == 0 ? 1 : -1;

                break;
            default:
                break;
        }
    }

}