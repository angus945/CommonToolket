using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ModdingLaboratory.Instance.Componentized
{
    public class DebugFlag : MonoBehaviour
    {
        string flag;

        public void Initial(string flag)
        {
            this.flag = flag;
        }
    }
}
