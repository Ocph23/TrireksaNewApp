using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsShared
{
    public interface IModelValidate
    {
        bool IsValid { get; }
        bool ValidatedAction();

    }
}
