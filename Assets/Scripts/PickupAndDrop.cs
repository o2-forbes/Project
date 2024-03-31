using UnityEngine;

public class PickupAndDrop : MonoBehaviour
{
    private GameObject item; 
    private bool isCarrying = false; 
    private Transform itemHolder; 

    void Start()
    {
        itemHolder = transform.Find("ItemHolder"); 
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!isCarrying)
            {
                PickUpItem();
            }
            else
            {
                DropItem();
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (!isCarrying && other.CompareTag("Item"))
        {
            item = other.gameObject;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (!isCarrying && other.CompareTag("Item"))
        {
            item = null;
        }
    }

    void PickUpItem()
    {
        if (item != null)
        {
            item.transform.SetParent(itemHolder);
            
            item.transform.localPosition = Vector3.zero;
            item.transform.localRotation = Quaternion.identity;
            
            Rigidbody itemRigidbody = item.GetComponent<Rigidbody>();
            if (itemRigidbody != null)
            {
                itemRigidbody.isKinematic = true;
            }
           
            isCarrying = true;
        }
    }

    void DropItem()
    {
        if (item != null)
        {
           
            item.transform.SetParent(null);
            
            Rigidbody itemRigidbody = item.GetComponent<Rigidbody>();
            if (itemRigidbody != null)
            {
                itemRigidbody.isKinematic = false;
            }
            
            isCarrying = false;
            
            item = null;
        }
    }
}
