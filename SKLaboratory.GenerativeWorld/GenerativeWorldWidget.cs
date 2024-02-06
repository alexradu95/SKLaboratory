using SKLaboratory.Core.BaseClasses;

namespace VRWorld
{
    public class GenerativeWorldWidget : BaseWidget
    {
        List<VRWorld.Object> objects = new List<VRWorld.Object>();

        public void AddObject(Object obj)
        {
            objects.Add(obj);
        }

        public override void OnFrameUpdate()
        {
            foreach(VRWorld.Object o in objects)
            {
                o.Draw();
            }
        }
    }
}