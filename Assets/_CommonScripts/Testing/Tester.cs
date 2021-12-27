using System.Collections;
using System.Collections.Generic;
using Toolket.Interface;
using Toolket.Optional;
using UnityEngine;

public interface ITestInterface
{
    int value { get; }

}
public class Tester : MonoBehaviour
{

    //[SerializeField] OptionalField<int>[] optionalInt = new OptionalField<int>[2];

    //[Space]
    //[SerializeField] InterfaceField<ITestInterface> interfaceField = new InterfaceField<ITestInterface>();
    //[SerializeField] InterfaceField<ITestInterface>[] interfaceFieldArray = new InterfaceField<ITestInterface>[1];

    void Start()
    {
        //for (int i = 0; i < interfaceFieldArray.Length; i++)
        //{
        //    var target = interfaceFieldArray[i];
        //    Debug.Log(target.element.value);
        //}
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
