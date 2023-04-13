using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Common.Mappings
{
  public class IMapFrom<T>
  {
    public void Mapping(Profile profile) => 
      profile.CreateMap(typeof(T), GetType());
  }
}
