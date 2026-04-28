using System;
using Game339.Shared.Infrastructure;
using UnityEngine;

namespace Game.Runtime
{
    [Serializable]
    public class ObservableInt : ObservableValue<int>, ISerializationCallbackReceiver
    {
        [SerializeField] private int initialValue;

        public void OnAfterDeserialize()  => Value = initialValue;
        public void OnBeforeSerialize()   => initialValue = Value;
    }
}