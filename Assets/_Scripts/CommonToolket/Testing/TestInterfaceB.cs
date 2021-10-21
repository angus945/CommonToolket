using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestInterfaceB : MonoBehaviour, ITestInterface
{
    [SerializeField] int _value;
    public int value => _value;


}
