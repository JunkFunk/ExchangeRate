using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;

namespace Common.Mapper
{
    public class Mapper<TInput, TOutput>
    {
        private readonly MapperConfiguration _configuration;

        public Mapper() : this(configMap => { })
        {
        }

        public Mapper(Action<IMappingExpression<TInput, TOutput>> configuration)
        {
            _configuration = new MapperConfiguration(cfg =>
            {
                var configMap = cfg.CreateMap<TInput, TOutput>();
                configuration(configMap);
            });
        }

        public TOutput Map(TInput input)
        {
            var mapper = _configuration.CreateMapper();
            return mapper.Map<TOutput>(input);
        }

        public IEnumerable<TOutput> Map(IEnumerable<TInput> inputs)
        {
            return inputs.Select(Map);
        }
    }
}