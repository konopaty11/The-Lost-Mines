using System.Collections.Generic;
using UnityEngine;

public class PlayerTriggerController : MonoBehaviour
{
    public List<Resource> Resources => _resources;

    List<Resource> _resources = new();
    string _resourceTag = "Resource";


    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        if (!other.CompareTag(_resourceTag)) return;
        Debug.Log(other + " enter");
        Resource _resource = other.GetComponent<Resource>();
        if (_resource != null)
            _resources.Add(_resource);
    }

    void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag(_resourceTag)) return;
        Debug.Log(other + " exit");

        Resource _resource = other.GetComponent<Resource>();
        if (_resource != null)
            _resources.Remove(_resource);
    }
}
