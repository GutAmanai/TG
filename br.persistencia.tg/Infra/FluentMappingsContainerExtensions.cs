﻿using System.IO;
using System.Linq;
using System.Reflection;
using FluentNHibernate.Cfg;

namespace br.persistencia.tg.Infra
{
    public static class FluentMappingsContainerExtensions
    {
        public static FluentMappingsContainer AddFromAssembliesInPath(this FluentMappingsContainer container, string path)
        {
            var assemblies =
                Directory
                    .EnumerateFiles(path, "*.dll", SearchOption.AllDirectories)
                    .Where(filename => Path.GetFileName(filename).ToLowerInvariant().Contains("persistencia")
                                    && !Path.GetFileName(filename).ToLowerInvariant().Contains("test"))
                    .Select(Assembly.LoadFile);

            foreach (var assembly in assemblies)
                container.AddFromAssembly(assembly);

            return container;
        }
    }
}
