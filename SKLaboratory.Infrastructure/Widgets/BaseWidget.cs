using SKLaboratory.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SKLaboratory.Infrastructure.Widgets
{
    public abstract class BaseWidget : IWidget
    {
        public bool IsEnabled = false;

        public abstract void Init();

        public abstract void Shutdown();

        public abstract void Update();
    }
}
