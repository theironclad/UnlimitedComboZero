using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour {

    public float speed;

    public EnemyController ec;
    private GameManager gmi;

    void Start()
    {
        gmi = GameManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        Destroy(gameObject, 8f);
    }

    private void OnCollisionEnter(Collision col)
    {
        GameObject go = col.gameObject;
        if (go.GetComponent<PlayerController>())
        {
            ec.DoDamage(2);
            Destroy(gameObject);
        }

        if (go.GetComponent<EnemyProjectile>())
        {
            Physics.IgnoreCollision(go.GetComponent<Collider>(), GetComponent<Collider>());
        }
    }   
}
