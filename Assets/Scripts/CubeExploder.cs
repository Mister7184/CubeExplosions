using UnityEngine;

public class CubeExploder : MonoBehaviour
{
    [SerializeField] private CubeFactory _factory;    
    [SerializeField] private float _explosionForce = 5f;
    [SerializeField] private float _explosionRadius = 2f;
    
    private float _splitChance = 1f;
    private float _divisorSplitChance = 0.5f;
    private float _scaleFactor = 0.5f;
    private int _minChildren = 2;
    private int _maxChildren = 6;


    private void OnMouseDown()
    {
        if (Random.value > _splitChance)
        {
            Destroy(gameObject);
            
            return;
        }

        int childCount = Random.Range(_minChildren, _maxChildren + 1);
        float childChance = Mathf.Clamp01(_splitChance * _divisorSplitChance);

        for (int i = 0; i < childCount; i++)
        {
            Vector3 childScale = transform.localScale * _scaleFactor;
            GameObject childInstance = _factory.Spawn(transform.position, Random.rotation, childScale);

            CubeExploder childExploder = childInstance.GetComponent<CubeExploder>();
            childExploder.SetSplitChance(childChance);
            childExploder.SetFactory(_factory);

            Exploder childPhysics = childInstance.GetComponent<Exploder>();
            childPhysics.ExplodeFrom(transform.position, _explosionForce, _explosionRadius);
        }

        Destroy(gameObject);
    }

    public void SetSplitChance(float newChance)
    {
        _splitChance = Mathf.Clamp01(newChance);
    }

    public void SetFactory(CubeFactory newFactory)
    {
        _factory = newFactory;
    }
}
