using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestInterfaceA : MonoBehaviour, ITestInterface
{
    public int value => 0;
}
