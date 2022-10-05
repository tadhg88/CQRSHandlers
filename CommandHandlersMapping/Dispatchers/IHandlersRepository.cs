using System;
using System.Collections.Generic;
using System.Text;

namespace CommandHandlersMapping.Dispatchers
{
    public interface IHandlersRepository
    {
        object FindByCommandType(Type commandType, Type resultType = null);
    }
}
