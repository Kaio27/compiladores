
namespace compilador
{
	
	public class Tokens
	{

		public static int Covert(string type)
		{
			if (type.Equals("ERR"))
			{
				return -2;
			}
			else if (type.Equals("EOL"))
			{
				return 256;
			}
			else if (type.Equals("PLUS"))
			{
				return 257;
			}
			else if (type.Equals("EQUALS"))
			{
				return 258;
			}
			else if (type.Equals("NUM"))
			{
				return 259;
			}
			else if (type.Equals("VAR"))
			{
				return 260;
			}
			else if (type.Equals("PRINT"))
			{
				return 261;
			}

			return -1;
		}
	}

}