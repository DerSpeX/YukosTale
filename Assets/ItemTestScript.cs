using UnityEngine;

public class ItemTestScript : MonoBehaviour
{
    private PlayerStatHandler _playerStatHandler;
    public float minExperience;
    public float maxExperience; 
    private void Start()
    {
        _playerStatHandler = FindObjectOfType<PlayerStatHandler>();
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            _playerStatHandler.AddExperience(Random.Range(minExperience, maxExperience));
        }
    }
}
