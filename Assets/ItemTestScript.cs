using UnityEngine;

public class ItemTestScript : MonoBehaviour
{
    private CharacterStatHandler _characterStatHandler;
    public float minExperience;
    public float maxExperience; 
    private void Start()
    {
        _characterStatHandler = FindObjectOfType<CharacterStatHandler>();
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            _characterStatHandler.AddExperience(Random.Range(minExperience, maxExperience));
        }
    }
}
