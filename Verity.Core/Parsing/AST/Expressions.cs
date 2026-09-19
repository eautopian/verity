using Verity.Core.Text;

namespace Verity.Core.Parsing.AST;

public class NumberExpression(int value) : ASTNode
{
	public int Value { get; } = value;
}

public class BinaryExpression(ASTNode left, SyntaxKind operation, ASTNode right) : ASTNode
{
	public ASTNode Left = left;
	public SyntaxKind Operation = operation;
	public ASTNode Right = right;
}

