﻿using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityAccess.Service
{
    public static class IOCManager
    {
        #region Declarations
        private static CompositionContainer _Container;
        #endregion Declarations

        public static T Resolve<T>()
        {
           return Container.GetExportedValue<T>();
        }

        private static CompositionContainer Container
        {
            get
            {
                if (_Container == null)
                {
                    var catalog =
                        new DirectoryCatalog(@"..\..\..\..\..\Output\", "*.dll");

                    _Container = new CompositionContainer(catalog);
                    
                }

                return _Container;
            }
        }
    }
}
