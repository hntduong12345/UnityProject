using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Tilemaps;
using UnityEngine;

public class DriverScript : MonoBehaviour, IDataPersistence
{
    #region Movement
    private float horizontalInput;
    private float verticalInput;

    [SerializeField]
    private float moveSpeed;
    private float defaultMoveSpeed;
    private float driftSpeed;

    [SerializeField]
    private float speedBoostPercent;
    [SerializeField]
    private float boostDuration;
    #endregion

    #region TaskAttribute
    //Task value
    private int currentTaskValue;
    private List<PackageScript> packagesCarried;

    //Car tranformation (base on current task value)
    private SpriteRenderer spriteRenderer;
    [SerializeField]
    private Sprite car1;
    [SerializeField]
    private Sprite car2;
    [SerializeField]
    private Sprite car3;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        defaultMoveSpeed = moveSpeed;
        driftSpeed = moveSpeed * 12;
        currentTaskValue = 0;
        speedBoostPercent = (100 + speedBoostPercent) / 100;
        packagesCarried = new List<PackageScript>();

        LoadPackageCarriedData();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        //Check is task completed
        GameManagerScript.instance.CheckTask(currentTaskValue);
    }

    private void FixedUpdate()
    {
        transform.Translate(0, verticalInput * moveSpeed * Time.deltaTime, 0);
        transform.Rotate(0, 0, -horizontalInput * driftSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var triggeredObject = collision.tag;

        switch (triggeredObject)
        {
            case "SpeedUp":
                SpeedBoost();
                ChangeCurrentCarState(2);
                FadingSpeedUpToken(collision.gameObject);
                break;
            case "Package":
                ChangeCurrentCarState(1);
                PickUpPackage(collision.gameObject);

                GameManagerScript.instance.UpdateCarriedPackageValue(packagesCarried.Count);
                break;
            case "Customer":
                ChangeCurrentCarState(0);
                currentTaskValue += packagesCarried.Count;
                DeliveryPackageToCustomer();

                GameManagerScript.instance.UpdateTaskProgress(currentTaskValue);
                GameManagerScript.instance.UpdateCarriedPackageValue(packagesCarried.Count);
                break;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (defaultMoveSpeed != moveSpeed)
        {
            ResetSpeed();
            ChangeCurrentCarState(0);
        }
    }

    #region SpeedChange
    private void SpeedBoost()
    {
        moveSpeed *= speedBoostPercent;
    }

    private void ResetSpeed()
    {
        moveSpeed /= speedBoostPercent;
    }
    #endregion

    #region SpeedToken
    private void FadingSpeedUpToken(GameObject speedUpToken)
    {
        speedUpToken.SetActive(false);
        StartCoroutine(RespawnToken(speedUpToken));
    }

    private IEnumerator RespawnToken(GameObject speedUpToken)
    {
        yield return new WaitForSeconds(5);
        speedUpToken.SetActive(true);
    }
    #endregion

    #region Package
    private void PickUpPackage(GameObject package)
    {
        packagesCarried.Add(package.GetComponent<PackageScript>());
    }

    private void DeliveryPackageToCustomer()
    {
        foreach (var package in packagesCarried)
        {
            package.deliverToCustomer();
        }

        packagesCarried.Clear();
    }
    #endregion

    #region SaveLoadData
    private void ChangeCurrentCarState(int status)
    {
        switch (status)
        {
            case 1:
                spriteRenderer.sprite = car1;
                break;
            case 2:
                spriteRenderer.sprite = car2;
                break;
            default:
                spriteRenderer.sprite = car3;
                break;
        }
    }

    public void LoadData(GameData data)
    {
        this.transform.position = data.playerPosition;
        this.transform.rotation = Quaternion.Euler(data.playerRotation);
    }

    public void SaveData(ref GameData data)
    {
        data.playerPosition = this.transform.position;
        data.playerRotation = this.transform.rotation.eulerAngles;
    }

    public void LoadPackageCarriedData()
    {
        GameObject[] packages = GameObject.FindGameObjectsWithTag("Package");
        foreach (var package in packages)
        {
            if (package.GetComponent<PackageScript>().IsCarried)
            {
                packagesCarried.Add(package.GetComponent<PackageScript>());
            }

            if (package.GetComponent<PackageScript>().IsDelivered)
            {
                currentTaskValue++;
            }
        }

        GameManagerScript.instance.UpdateCarriedPackageValue(packagesCarried.Count);
        GameManagerScript.instance.UpdateTaskProgress(currentTaskValue);
    }
    #endregion
}
