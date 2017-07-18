using H.Skeepy.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H.Skeepy.Core
{
    public class ClashUseCase
    {
        private readonly Clash clash;

        public ClashUseCase(Clash clash)
        {
            this.clash = clash ?? throw new InvalidOperationException("The Clash Use-Case must have an underlying Clash");
        }
    }
}
