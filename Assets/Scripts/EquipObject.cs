using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EquipObject : MonoBehaviour
{
    public GameObject Object;
    public Transform ObjectParent;
    public TextMeshProUGUI pickupText;

    void Start()
    {
        Object.GetComponent<Rigidbody>().isKinematic = true;
        pickupText.gameObject.SetActive(false); // Initially hide the pickup text
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            pickupText.gameObject.SetActive(true); // Show the pickup text when player enters the trigger
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            pickupText.gameObject.SetActive(false); // Hide the pickup text when player exits the trigger
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (Input.GetKey(KeyCode.E))
            {
                Equip();
                pickupText.gameObject.SetActive(false); // Hide the pickup text when player picks up the object
            }
        }
    }
}