using Infrastructure.DI;
using Services.Progress;
using UnityEngine;

namespace Logic
{
    public class SaveTrigger : MonoBehaviour
    {
        private ISaveLoadService _saveLoadService;

        [SerializeField] private BoxCollider _boxCollider;
        
        private void Awake()
        {
            _saveLoadService = DiContainer.Instance.Single<ISaveLoadService>();
        }

        private void OnTriggerEnter(Collider other)
        {
            _saveLoadService.SaveProgress();
            Debug.Log("Progress Saved");
            gameObject.SetActive(false);
        }

        private void OnDrawGizmos()
        {
            if (!_boxCollider) 
                return;
            
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(transform.position + _boxCollider.center, _boxCollider.size);
        }
    }
}