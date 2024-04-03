using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipObject : MonoBehaviour
{

    public GameObject Object;
    public Transform ObjectParent;
    
    void Start()
    {
        Object.GetComponent<Rigidbody>().isKinematic = true;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.F))
        {
            Drop();
        }
    }

    void Drop()
    {
        ObjectParent.DetachChildren();
        Object.transform.eulerAngles = new Vector3(Object.transform.position.x, Object.transform.position.z, Object.transform.position.y);
        Object.GetComponent<Rigidbody>().isKinematic = false;
        Object.GetComponent<MeshCollider>().enabled = true;
    }

    void Equip()
    {
        Object.GetComponent<Rigidbody>().isKinematic = true;

        Object.transform.position = ObjectParent.transform.position;
        Object.transform.rotation = ObjectParent.transform.rotation;

        Object.GetComponent<MeshCollider>().enabled = false;

        Object.transform.SetParent(ObjectParent);
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            if (Input.GetKey(KeyCode.E))
            {
                Equip();
            }
        }
    }
}
