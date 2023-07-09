using SKLaboratory.Infrastructure.Interfaces;
using StereoKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SKLaboratory.Infrastructure.Base
{
    public abstract class BaseWidget : IWidget
    {
        public Guid Id { get; private set; }
        public bool IsActive { get; protected set; }

        public abstract Matrix Transform { get; }

        public abstract Pose Pose { get; }

        public virtual bool Initialize()
        {
            Id = Guid.NewGuid();
            IsActive = true;
            return true;
        }

        public abstract void Shutdown();
        public abstract void Draw();
    }
}
