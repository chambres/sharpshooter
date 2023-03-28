using UnityEngine;
public class Explosive : MonoBehaviour {
    [SerializeField] private float _triggerForce = 0.5f;
    [SerializeField] private float _explosionRadius = 5;
    [SerializeField] private float _explosionForce = 500;
    [SerializeField] private GameObject _particles;


    public bool explode = false;
 

    void boom(){
        var surroundingObjects = Physics.OverlapSphere(transform.position, _explosionRadius);
 
            foreach (var obj in surroundingObjects) {
                var rb = obj.GetComponent<Rigidbody>();
                //set use gravity to true
                if (rb == null) continue;             
 
                rb.AddExplosionForce(_explosionForce, transform.position, _explosionRadius,1);
            }
 
            //Instantiate(_particles, transform.position, Quaternion.identity);
 
            Destroy(gameObject);
    }

    void Update(){
        if(explode){
            boom();
            explode = false;
        }
    }
}