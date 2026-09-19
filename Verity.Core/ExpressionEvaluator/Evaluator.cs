using Verity.Core.Parsing.AST;
using Verity.Core.Text;

namespace Verity.Core.ExpressionEvaluator;

public sealed class ExpressionEvaluator(ASTNode node)
{
	private readonly ASTNode _node = node;

	private float EvaluteBinary(BinaryExpression binary)
	{
		var left = EvaluateExpression(binary.Left);
		var right = EvaluateExpression(binary.Right);

		float? value = binary.Operation switch
		{
			SyntaxKind.Minus => left - right,
			SyntaxKind.Plus => left + right,
			SyntaxKind.Star => left * right,
			SyntaxKind.RightSlash => left / right,
			_ => null,
		};
		if (value is float forcedFloat)
			return forcedFloat;

		throw new Exception("Unknown operator " + binary.Operation.ToString());
	}

	private float EvaluateExpression(ASTNode node)
	{
		if (node is NumberExpression numbExpression)
			return numbExpression.Value;
		if (node is BinaryExpression binaryExpression)
			return EvaluteBinary(binaryExpression);

		throw new Exception($"No expression {node}");
	}

	public float Evaluate() => EvaluateExpression(_node);
}

