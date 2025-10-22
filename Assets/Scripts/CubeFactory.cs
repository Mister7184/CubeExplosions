using UnityEngine;

public class CubeFactory : MonoBehaviour
{
    [SerializeField] private GameObject cubePrefab;

    public GameObject Spawn(Vector3 position, Quaternion rotation, Vector3 targetLocalScale)
    {
        GameObject cubeInstance = Instantiate(cubePrefab, position, rotation);
        cubeInstance.transform.localScale = targetLocalScale;

        cubeInstance.GetComponent<Renderer>().material.color = Random.ColorHSV();

        cubeInstance.GetComponent<CubeExploder>().SetFactory(this);

        return cubeInstance;
    }
}
