using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Configuration
{
    public static class AutoMapperConfiguration
    {
            public static IMapper mapper;

            public static IMapper Initialize()
            {
                var mappingConfig = new MapperConfiguration(mc =>
                {
                    mc.AddProfile(new RegisterMappers());
                });

                mapper = mappingConfig.CreateMapper();

                return mapper;
            }
        }

        public static class GenericAutoMapper
        {
            public static Model ToModel<Model, Domain>(this Domain entity)
            {
                var _mapper = AutoMapperConfiguration.mapper;
                return _mapper.Map<Model>(entity);
            }

            public static Domain ToDomain<Domain, Model>(this Model entity)
            {
                var _mapper = AutoMapperConfiguration.mapper;
                return _mapper.Map<Domain>(entity);
            }
        } 
}
