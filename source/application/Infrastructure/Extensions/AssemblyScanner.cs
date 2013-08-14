using System;
using System.IO;
using System.Reflection;
using System.Collections.Generic;
using Intrigma.DonorSpace.Infrastructure.Configuration;

namespace Intrigma.DonorSpace.Infrastructure.Extensions
{
    public static class AssemblyScanner
    {
        public static Assembly FromAssemblyContaining<T>()
        {
            return typeof (T).Assembly;
        }

        public static Assembly FromAssemblyContaining(Type type)
        {
            return type.Assembly;
        }

        
        public static IEnumerable<Type> GetExportedTypesFrom(Assembly assembly)
        {
            return assembly.GetExportedTypes();
        }

        public static IEnumerable<Type> GetExportedTypesFromAssemblyContaining<T>()
        {
            return  GetExportedTypesFrom(typeof(T).Assembly);
        }

        public static IEnumerable<Type> GetExportedTypesFromAssemblyNamed(string assemblyName)
        {
            return GetExportedTypesFrom(FromAssemblyNamed(assemblyName));
        }

        public static IEnumerable<Type> GetExportedTypesFromWebAssembly()
        {
            return GetExportedTypesFromAssemblyNamed(ConfigSettings.WebAssemblyName);
        }
       
        public static Assembly FromAssemblyNamed(string assemblyName)
        {
            if (!IsAssemblyFile(assemblyName)) return Assembly.Load(assemblyName);


            if (Path.GetDirectoryName(assemblyName) != AppDomain.CurrentDomain.BaseDirectory)
            {
                return Assembly.LoadFile(assemblyName);
            }

            return Assembly.Load(Path.GetFileNameWithoutExtension(assemblyName));
            
        }

        public static Assembly FromWebAssembly()
        {
            return FromAssemblyNamed(ConfigSettings.WebAssemblyName);
        }

        public static bool IsAssemblyFile(string filePath)
        {
            if (filePath == null)
                throw new ArgumentNullException("filePath");
            string extension;
            try
            {
                extension = Path.GetExtension(filePath);
            }
            catch (ArgumentException)
            {
                return false;
            }
            return IsDll(extension) || IsExe(extension);
        }

        private static bool IsExe(string extension)
        {
            return ".exe".Equals(extension, StringComparison.OrdinalIgnoreCase);
        }

        private static bool IsDll(string extension)
        {
            return ".dll".Equals(extension, StringComparison.OrdinalIgnoreCase);
        }
    }
}
