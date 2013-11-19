// Copyright (c) James Dingle

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Primer;
using Moq;

namespace Primer.Mocking
{
    class Helper
    {

        public static Mock<IViewModel> MockViewModel()
        {
            var mock = new Mock<IViewModel>();

            mock.Setup(m => m.Dispose()).Throws(new ApplicationException());

            return mock;
        }
    }
}
