using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uiscriptengine;

namespace scriptengine.test
{
    public class TestCodeGen
    {
        [Test]
        public void TestGenClass()
        {
            var emptyClassTree = new EventHanlderGenerator("XXX_EventHandler");
            emptyClassTree.addEventHandler("button1", "Click", typeof(EventArgs));
            emptyClassTree.addEventHandler("button2", "Click", typeof(EventArgs));
            emptyClassTree.removeEventHandler("button1", "Click");
            Console.WriteLine(emptyClassTree.GetSyntaxTree().GetRoot().NormalizeWhitespace().ToFullString());
        }

    }
}
