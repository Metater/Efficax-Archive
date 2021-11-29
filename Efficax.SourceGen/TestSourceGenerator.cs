using System;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Efficax.SourceGen
{
    [Generator]
    public class TestSourceGenerator : ISourceGenerator
    {
        public void Initialize(GeneratorInitializationContext context)
        {
            context.RegisterForSyntaxNotifications(() => new TestSyntaxReceiver());
        }
        public void Execute(GeneratorExecutionContext context)
        {
            TestSyntaxReceiver syntaxReceiver = (TestSyntaxReceiver)context.SyntaxReceiver;

            ClassDeclarationSyntax userClass = syntaxReceiver.ClassToAugment;
            if (userClass is null) return;
        }

        class TestSyntaxReceiver : ISyntaxReceiver
        {
            public ClassDeclarationSyntax ClassToAugment { get; private set; }

            public void OnVisitSyntaxNode(SyntaxNode syntaxNode)
            {
                if (syntaxNode is ClassDeclarationSyntax cds && cds.Identifier.ValueText == "UserClass")
                {
                    ClassToAugment = cds;
                }
            }
        }
    }
}
