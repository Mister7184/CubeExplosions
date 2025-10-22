using UnityEngine;

public class CubeExploder : MonoBehaviour
{
    [SerializeField] private CubeFactory factory;    
    [SerializeField] private float explosionForce = 5f;
    [SerializeField] private float explosionRadius = 2f;
    
    private float splitChance = 1f;
    private float divisorSplitChance = 0.5f;
    private float scaleFactor = 0.5f;
    private int minChildren = 2;
    private int maxChildren = 6;


    private void OnMouseDown()
    {
        if (Random.value > splitChance)
        {
            Destroy(gameObject);
            
            return;
        }

        int childCount = Random.Range(minChildren, maxChildren + 1);
        float childChance = Mathf.Clamp01(splitChance * divisorSplitChance);

        for (int i = 0; i < childCount; i++)
        {
            Vector3 childScale = transform.localScale * scaleFactor;
            GameObject childInstance = factory.Spawn(transform.position, Random.rotation, childScale);

            CubeExploder childExploder = childInstance.GetComponent<CubeExploder>();
            childExploder.SetSplitChance(childChance);
            childExploder.SetFactory(factory);

            Exploder childPhysics = childInstance.GetComponent<Exploder>();
            childPhysics.ExplodeFrom(transform.position, explosionForce, explosionRadius);
        }

        Destroy(gameObject);
    }

    public void SetSplitChance(float newChance)
    {
        splitChance = Mathf.Clamp01(newChance);
    }

    public void SetFactory(CubeFactory newFactory)
    {
        factory = newFactory;
    }
}
