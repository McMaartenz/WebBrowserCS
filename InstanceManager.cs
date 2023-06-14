using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WebBrowser
{
    public class InstanceManager
    {
        private List<Window> _activeInstances = new();
        private List<Type> _instanceTypes = new();

        public void RegisterInstance(Window instance)
        {
            _activeInstances.Add(instance);
            _instanceTypes.Add(instance.GetType());
            instance.Closing += Instance_Closing;
        }

        public void UnregisterInstance(Window instance)
        {
            int i = _activeInstances.IndexOf(instance);
            if (i == -1)
            {
                throw new Exception("Unregistering a non-registered instance");
            }

            _activeInstances.RemoveAt(i);
            _instanceTypes.RemoveAt(i);
        }

        private void Instance_Closing(object? sender, CancelEventArgs e)
        {
            if (sender is Window instance)
            {
                UnregisterInstance(instance);
            }
        }
    }
}
