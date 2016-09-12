using System.Windows.Forms;
using Bedrock.Regions;
using Bedrock.Views;

namespace Bedrock.Winform
{
    public class Region : RegionBase
    {
        public Region(object control) : base(control)
        {
        }

        public Region(string name, object control) : base(name, control)
        {
        }

        public override void Activate(IView view)
        {
            base.Activate(view);
            var castedView = view as Control;
            var container = Control as Control;
            if (castedView != null && container != null)
            {
                container.Controls.Add(castedView);
                castedView.Visible = true;
            }
        }

        public override void Deactivate(IView view)
        {
            base.Deactivate(view);
            var castedView = view as Control;
            if (castedView != null)
            {
                castedView.Visible = false;
            }
        }

        public override void Remove(IView view)
        {
            var castedView = view as Control;
            var container = Control as Control;
            if (castedView != null && container != null)
            {
                container.Controls.Remove(castedView);
                ActiveViews.Remove(view);
                Views.Remove(view);
            }
        }
    }
}
