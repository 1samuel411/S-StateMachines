using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLibrary.StateExample
{
    [System.Serializable]
    public class Bindable<BindableType> : IBindable<BindableType>
    {

        public event BindableUpdated<BindableType> OnUpdate;

        private BindableType value;

        public Bindable(BindableType initialValue)
        {
            value = initialValue;
        }

        public void SetValue(BindableType newValue)
        {
            if(value.Equals(newValue) == false)
            {
                value = newValue;
                OnUpdate?.Invoke(value);
            }
        }
        
        public BindableType GetValue()
        {
            return value;
        }

    }
}
