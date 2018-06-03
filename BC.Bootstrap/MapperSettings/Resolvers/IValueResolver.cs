using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace BC.Bootstrap.MapperSettings.Resolvers
{
    public interface IValueResolver<in TSource, in TDestination, TDestMember>
    {
        TDestMember Resolve(TSource source, TDestination destination, TDestMember destMember, ResolutionContext context);
    }
}
