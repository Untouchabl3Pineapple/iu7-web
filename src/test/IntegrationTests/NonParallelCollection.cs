using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationTests
{
    [CollectionDefinition(nameof(NonParallelCollection), DisableParallelization = true)]
    public class NonParallelCollection
    {
    }
}
