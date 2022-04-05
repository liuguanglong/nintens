using Microsoft.CodeAnalysis;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using uiscriptengine;

namespace scriptengine.test
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestBuildAssembly()
        {
            ScriptBuilder builder = new ScriptBuilder();
            string basescript = @"
                           namespace uiscriptengine
                            {
                                using System;
                                using System.Collections.Generic;
                                using System.Linq;
                                using System.Text;
                                using System.Threading.Tasks;

                                public partial class TestHanlder : BaseEventHanlder {
                                    FormController controller;
                                    public TestHanlder(FormController controller)
                                    {
                                         this.controller = controller;
                                    }
                                }
                           }
                    ";

            string script = @"
                           namespace uiscriptengine
                            {
                                using System;
                                using System.Collections.Generic;
                                using System.Linq;
                                using System.Text;
                                using System.Threading.Tasks;

                                public partial class TestHanlder {
                                    [EventAttribute(""button2"",""click"")]
                                    public async Task button1_click(EventArgs args)
                                    {
                                         System.Console.WriteLine(""Event Called"");
                                    }
                                }
                           }
                    ";
            String assemblyName = "TestHanlder.dll";
            var result = builder.BuildScriptAssembly(basescript,script, assemblyName);
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
                String path = Path.Combine(BasePath, "TestHanlder.dll");
                var asm = Assembly.LoadFrom(path);
                var scriptType = asm.GetType("uiscriptengine.TestHanlder");
                FormController controller = new FormController();   
                if (scriptType != null)
                {
                    BaseEventHanlder? runnable = (BaseEventHanlder?)Activator.CreateInstance(scriptType, controller);
                    EventArgs eventArgs = new EventArgs();
                    runnable?.Handle("button2", "click", eventArgs);
                }
        }
    }
}