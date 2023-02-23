using dnlib.DotNet;
using dnlib.DotNet.Emit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ByteArrayTest
{
    internal class Program
    {
        private static MethodInfo GetMethod(Type x)
        {
            return x.GetMethod("Main", EnoughFalgs, null, default, new Type[] { Type.GetType("System.String[]") }, default);
        }


        static void Main(string[] args)
        {
            Assembly assembly = Assembly.LoadFile(args[0]);
            ModuleDef module = ModuleDefMD.Load(assembly.ManifestModule);
            foreach (var item in assembly.ManifestModule.GetTypes())
            {
                foreach (var method in item.GetMethods())
                {
                    Console.WriteLine($"Name:{method.Name}  codesize:{method.GetMethodBody()?.GetILAsByteArray().Length}\t {method.GetMethodBody()?.GetILAsByteArray().ToHex()}");
                    SDILReader.MethodBodyReader mr = new SDILReader.MethodBodyReader(method);
                    Console.WriteLine(mr.GetBodyCode());
                }
            }
            Console.ReadKey();
        }

        internal static System.Reflection.BindingFlags EnoughFalgs => System.Reflection.BindingFlags.InvokeMethod | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance;
    }
}
