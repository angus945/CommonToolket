using DataDriven;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ModdingLaboratory.Definition
{
    public abstract class DefinitionBase
    {
        public string globalID { get => $"{groupID}.{localID}"; }

        protected abstract string localID { get; }

        string groupID;
        public virtual void SetGroup(string group)
        {
            groupID = group;
        }
        protected string ApplyGroupID(string value)
        {
            if (value.Contains("."))
            {
                return value;
            }
            else
            {
                return $"{groupID}.{value}";
            }
        }
    }

}
