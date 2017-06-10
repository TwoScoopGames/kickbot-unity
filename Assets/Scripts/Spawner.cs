using Random = UnityEngine.Random;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

  public GameObject[] prefabs;

  // Use this for initialization
  void Start() {
    var prefab = prefabs[Random.Range(0, prefabs.Length)];
    GameObject instance = Instantiate(prefab, new Vector3(0, 0, 0f), Quaternion.identity);
  }

  // Update is called once per frame
  void Update() {
    
  }
}
