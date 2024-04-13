using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitching : MonoBehaviour
{
    public int selectedWeapon = 1;
    public int maxWeapons = 4;
    //public Transform position = null;
    private List<GameObject> weapons = new List<GameObject>();
    void Start()
    {
        SelectWeapon(0);
    }

    void Update()
    {
        int previousSelectedWeapon = selectedWeapon;
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SelectWeapon(0);
            Debug.Log("Pistol " + selectedWeapon);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && transform.childCount >= 2)
        {
            SelectWeapon(1);
            Debug.Log("Rifle " + selectedWeapon);
        }
    }

    void SelectWeapon(int i)
    {
        Destroy(transform.GetChild(0));
        Instantiate(weapons[i], this.transform);
    }
}
