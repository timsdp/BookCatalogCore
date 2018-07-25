using System;
using System.Collections.Generic;
using System.Text;

namespace BC.Infrastructure.Mapper
{
    public interface IMapperService
    {
        TOut MapTo<TOut, TIn>(TIn entity)
            where TOut : class
            where TIn : class;

        IEnumerable<TOut> MapTo<TOut, TIn>(IEnumerable<TIn> entity)
            where TOut : class
            where TIn : class;
    }
}
