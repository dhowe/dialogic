using System;

namespace Dialogic
{
    public interface IChosen
    {
        Opt GetChosen();
    }

    public class ChosenEvent :EventArgs, IChosen
    {
        protected Opt option;

        public ChosenEvent(Opt option)
        {
            this.option = option;
        }

        public override string ToString()
        {
            return "Response: "+option.Text;
        }

        public Opt GetChosen()
        {
            return option;
        }
    }
}
