using System;

namespace Dialogic
{
    public class ResponseEvent : EventArgs
    {
        protected Opt option;

        public ResponseEvent(Opt option)
        {
            this.option = option;
        }

        public Opt Selected
        {
            get
            {
                return option;
            }
        }

        public override string ToString()
        {
            return "Response: "+option.Text;
        }

    }
}
