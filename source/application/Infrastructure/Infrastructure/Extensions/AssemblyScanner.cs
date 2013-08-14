using System;
using System.IO;
using System.Reflection;
using System.Collections.Generic;


namespace Panzea.DonorSpace.Infrastructure.Extensions
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

        public static Assembly FromThisAssembly()
        {
            return Assembly.GetCallingAssembly();
        }

        public static IEnumerable<Type> GetExportedTypesFromAssemblyContaining<T>()
        {
            return typeof(T).Assembly.GetExportedTypes();
        }

        public static IEnumerable<Type> GetExportedTypesFromAssemblyNamed(string assemblyName)
        {
            return FromAssemblyNamed(assemblyName).GetExportedTypes();
        }



        public static IEnumerable<Type> GetExportedTypesFromThisAssembly()
        {
            return FromThisAssembly().GetExportedTypes();
        }

        public static Assembly FromAssemblyNamed(string assemblyName)
        {
            return !IsAssemblyFile(assemblyName) ? Assembly.Load(assemblyName) : (!(Path.GetDirectoryName(assemblyName) == AppDomain.CurrentDomain.BaseDirectory) ? Assembly.LoadFile(assemblyName) : Assembly.Load(Path.GetFileNameWithoutExtension(assemblyName)));
           
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
