using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivateSelf : MonoBehaviour
{
    [SerializeField] private GameObject self;
    public void deactivate ()
    {
        self.SetActive(false);
    }
}
