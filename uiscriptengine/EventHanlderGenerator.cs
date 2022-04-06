using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace uiscriptengine
{
    public class EventHanlderGenerator
    {
        SyntaxNode root;

        public EventHanlderGenerator(string formname,string id)
        {
            var code = @"
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
            var syntaxTree = CSharpSyntaxTree.ParseText(code.Replace("TestHanlder", $"{formname}_{id}"));
            root = syntaxTree.GetRoot();
        }

        public EventHanlderGenerator(string formname, string id,string source)
        {
            var syntaxTree = CSharpSyntaxTree.ParseText(source);
            var identifierToken = syntaxTree.GetRoot().DescendantTokens()
                .First(t => t.IsKind(SyntaxKind.IdentifierToken)
                && t.Parent.Kind() == SyntaxKind.ClassDeclaration);
            if(identifierToken.ToString() != $"{formname}_{id}")
            {
                var newIdentifier = SyntaxFactory.Identifier($"{formname}_{id}");
                root = syntaxTree.GetRoot().ReplaceToken(identifierToken, newIdentifier);
            }
        }

        public void addEventHandler(String controlName, String eventName, Type eventType)
        {
            var emptyClass =
                 root.DescendantNodes().
                 OfType<ClassDeclarationSyntax>().FirstOrDefault();
            if (emptyClass == null)
                return;

            var method = CrateEventHanlder(controlName,eventName,eventType);
            var newClass = emptyClass.AddMembers(method);

            root = root.ReplaceNode(emptyClass, newClass);
        }


        public void removeEventHandler(String controlName, String eventName)
        {
            var emptyClass =
                 root.DescendantNodes().
                 OfType<ClassDeclarationSyntax>().FirstOrDefault();
            if (emptyClass == null)
                return;

            IEnumerable<MethodDeclarationSyntax> methods = root.DescendantNodes().OfType<MethodDeclarationSyntax>();
            foreach(var method in methods)
            {
                if(method.AttributeLists.Count == 1)
                {
                    var node = method.AttributeLists.First().Attributes.First();
                    var identity = node.Name.ToString();
                    if(identity == "EventAttribute")
                    {
                        if(node.ArgumentList.Arguments.ToString() == $"\"{controlName}\",\"{eventName}\""
                            )
                        {
                                var newClass = emptyClass.RemoveNode(method, SyntaxRemoveOptions.AddElasticMarker);
                                root = root.ReplaceNode(emptyClass, newClass);
                        }
                    }
                }
            }
        }


        public String GetSource()
        {
            return SyntaxFactory.SyntaxTree(root).GetRoot().NormalizeWhitespace().ToFullString();
        }

        private MethodDeclarationSyntax CrateEventHanlder(String controlName,String eventName,Type eventType)
        {
            var arguments = SyntaxFactory.ParseAttributeArgumentList($"(\"{controlName}\",\"{eventName}\")");

            var eventHanlder = SyntaxFactory.MethodDeclaration(SyntaxFactory.ParseTypeName("Task"), $"{controlName}_{eventName}")
                                        .AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword))
                                        .AddModifiers(SyntaxFactory.Token(SyntaxKind.AsyncKeyword))
                                        .AddParameterListParameters(
                                                SyntaxFactory.Parameter(
                                                       SyntaxFactory.Identifier("args")).WithType(
                                                            SyntaxFactory.ParseTypeName(eventType.FullName)))
                                        .AddAttributeLists(
                                                SyntaxFactory.AttributeList().AddAttributes(
                                                    SyntaxFactory.Attribute(
                                                        SyntaxFactory.IdentifierName("EventAttribute"), arguments)))
                                        .WithBody(SyntaxFactory.Block());

            return eventHanlder;
        }
    }
}
