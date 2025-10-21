using UnityEngine;

public class CubeExploder : MonoBehaviour
{
    [SerializeField] private GameObject _cubePrefab;
    [SerializeField] private float _splitChance = 1f;
    [SerializeField] private float _divisorSplitChance = 0.5f;
    [SerializeField] private float _scaleFactor = 0.5f;
    [SerializeField] private int _minChildren = 2;
    [SerializeField] private int _maxChildren = 6;
    [SerializeField] private float _explosionForce = 5f;
    [SerializeField] private float _explosionRadius = 2f;

    private void OnMouseDown()
    {
        if (Random.value <= _splitChance) 
        {
            int count = Random.Range(_minChildren, _maxChildren + 1);

            for (int i = 0; i < count; i++) 
            {
                GameObject child = Instantiate(_cubePrefab, transform.position, Random.rotation);
                child.transform.localScale = transform.localScale * _scaleFactor;

                child.GetComponent<Renderer>().material.color = Random.ColorHSV();

                child.GetComponent<CubeExploder>().SetSplitChance(_splitChance * _divisorSplitChance);

                child.GetComponent<Rigidbody>().AddExplosionForce(_explosionForce, transform.position, _explosionRadius);
            }
        }

        Destroy(gameObject);
    }

    public void SetSplitChance(float newChance) 
    {
        _splitChance = Mathf.Clamp01(newChance);
    }
}
