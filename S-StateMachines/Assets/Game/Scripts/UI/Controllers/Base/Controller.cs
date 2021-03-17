using UnityEngine;

namespace SLibrary.StateExample
{
    /// <summary>
    /// Based off my implementation here, https://samuelarminana.com/index.php/2019/02/11/mvc-in-unitys-ui/
    /// </summary>
    /// <typeparam name="V"></typeparam>
    /// <typeparam name="M"></typeparam>
    public class Controller<V, M> : MonoBehaviour where V : View where M : Model
    {
        public V view;
        public M model;

        void OnEnable()
        {
            Enabled();
        }
        public virtual void Enabled() { }

        void OnDisable()
        {
            Disabled();
        }
        public virtual void Disabled() { }
    }

    public class Model
    {

    }

    public class View
    {

    }
}