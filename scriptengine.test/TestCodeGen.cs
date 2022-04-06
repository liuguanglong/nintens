using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using uiscriptengine;

namespace scriptengine.test
{
    public class TestCodeGen
    {
        String formname = "测试";
        String id = "20";

        [Test]
        public void TestGenClass()
        {
            var generator = new EventHanlderGenerator(formname, id);
            generator.addEventHandler("button1", "Click", typeof(EventArgs));
            generator.addEventHandler("button2", "Click", typeof(EventArgs));
            generator.removeEventHandler("button1", "Click");
            Console.WriteLine(generator.GetSource());

            ScriptBuilder builder = new ScriptBuilder();
            String assemblyName = $"{formname}_{id}.dll";
            var result = builder.BuildScriptAssembly(generator.GetSource(), assemblyName);
            if (!result.Success)
            {
                IEnumerable<Diagnostic> failures = result.Diagnostics.Where(diagnostic =>
                    diagnostic.IsWarningAsError ||
                    diagnostic.Severity == DiagnosticSeverity.Error);

                foreach (Diagnostic diagnostic in failures)
                {
                    Console.Error.WriteLine("{0}: {1}", diagnostic.Id, diagnostic.GetMessage());
                }
            }
        }

        [Test]
        public void TestLoadAssembly()
        {
            String BasePath = System.AppDomain.CurrentDomain.BaseDirectory;
            String path = Path.Combine(BasePath, $"{formname}_{id}.dll");
            var asm = Assembly.LoadFrom(path);

            var scriptType = asm.GetType($"uiscriptengine.{formname}_{ id}");
            FormController controller = new FormController();
            if (scriptType != null)
            {
                BaseEventHanlder? runnable = (BaseEventHanlder?)Activator.CreateInstance(scriptType, controller);
                EventArgs eventArgs = new EventArgs();
                runnable?.Handle("button2", "Click", eventArgs);
            }
        }

    }
}
