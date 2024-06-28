using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.VFX;

public class PackageScript : MonoBehaviour, IDataPersistence
{
    [SerializeField]
    private string id;

    private DriverScript driver;

    [ContextMenu("Generate guid for id")]
    private void GenerateGuid()
    {
        id = System.Guid.NewGuid().ToString();
    }

    private bool isCarried;
    private bool isDelivered;

    private void Start()
    {
        driver = GameObject.FindGameObjectWithTag("Player").GetComponent<DriverScript>();
        isCarried = false;
        isDelivered = false;
    }

    public bool IsCarried { get { return isCarried; } }
    public bool IsDelivered { get {  return isDelivered; } }

    private void Carried()
    {
        isCarried = true;
        this.gameObject.GetComponent<Renderer>().enabled = false;
        this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
    }

    public void deliverToCustomer()
    {
        isDelivered = true;
        isCarried = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Carried();
    }

    public void LoadData(GameData data)
    {
        data.packagesCarried.TryGetValue(id, out isCarried);
        data.packagesDeliveried.TryGetValue(id, out isDelivered);
        if(isCarried || isDelivered)
        {
            this.gameObject.GetComponent<Renderer>().enabled = false;
            this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    public void SaveData(ref GameData data)
    {
        //Save package isCarried Status
        if (data.packagesCarried.ContainsKey(id))
        {
            data.packagesCarried.Remove(id);
        }
        data.packagesCarried.Add(id, isCarried);

        //Save package isDelivered Status
        if (data.packagesDeliveried.ContainsKey(id))
        {
            data.packagesDeliveried.Remove(id);
        }
        data.packagesDeliveried.Add(id, isDelivered);
    }
}
