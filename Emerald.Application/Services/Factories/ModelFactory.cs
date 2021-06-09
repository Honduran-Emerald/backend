using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Emerald.Application.Services.Factories
{
    public interface IModelFactory<Source, Target>
    {
        Task<Target> Create(Source source);
    }

    public static class ModelFactoryExtensions
    {
        public static async Task<List<Target>> Create<Source, Target>(
            this IModelFactory<Source, Target> modelFactory,
            List<Source> sources)
        {
            List<Target> targets = new List<Target>();

            foreach (Source module in sources)
            {
                targets.Add(await modelFactory.Create(module));
            }

            return targets;
        }

        public static async Task<Target?> CreateNullable<Source, Target>(
            this IModelFactory<Source, Target> modelFactory,
            Source? source)
            => source == null ? default(Target) : await modelFactory.Create(source);
    }
}
