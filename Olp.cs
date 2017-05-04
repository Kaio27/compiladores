using System;


namespace compilador
{
	
	public class Olp
	{
		internal Token lookahead = new Token();
		internal Symbols sym = new Symbols();
		internal Tokens tok = new Tokens();
		internal Lexer lex = new Lexer();

		internal virtual void error(string msg)
		{
			Console.WriteLine("Syntax error on" + lex.line + ":" + lex.char_pos);
			Console.WriteLine(msg);
		}

		internal virtual void match(int type)
		{
			if (lookahead.token == type)
			{
				lookahead = lex.next_Token();
			}
			else
			{
				error("Not Match");
			}
		}

		internal virtual void prg()
		{
			cmd();
			pr2();
		}
		internal virtual void pr2()
		{
			if (lookahead.token == Tokens.Covert("EOL"))
			{
				match(Tokens.Covert("EOL"));
				prg();
			}
			else if (lookahead.token != Tokens.Covert("EOF"))
			{
				error("EOL Expected");
			}
		}

		internal virtual void cmd()
		{
			if (lookahead.token == Tokens.Covert("NUM"))
			{
				exp();
			}
			else if (lookahead.token == Tokens.Covert("VAR"))
			{
				atr();
			}
			else if (lookahead.token == Tokens.Covert("PRINT"))
			{
				@out();
			}
			else if (lookahead.token != Tokens.Covert("EOF"))
			{
				error("Unrecongnized Command");
			}
		}

		internal virtual int exp()
		{
			if (lookahead.token == Tokens.Covert("NUM"))
			{
				int x = lookahead.value;
				match(Tokens.Covert("NUM"));
				int y = rst();
				return x + y;
			}
			return 0;
		}

		internal virtual void atr()
		{
			if (lookahead.token == Tokens.Covert("VAR"))
			{
				int v = lookahead.value;
				match(Tokens.Covert("VAR"));
				match(Tokens.Covert("EQUALS"));
				int x = val();
				if (!sym.update_symbol(v, x))
				{
					error("Impossible assign value");
				}
				else
				{
					error("VAR Expected");
				}
			}
		}
		internal virtual void @out()
		{
			if (lookahead.token == Tokens.Covert("PRINT"))
			{
				match(Tokens.Covert("PRINT"));
				int x = val();
				Console.WriteLine(x);
			}
		}

		internal virtual int val()
		{
			if (lookahead.token == Tokens.Covert("VAR"))
			{
				Token v = lookahead;
				match(Tokens.Covert("VAR"));
				Token s = sym.get_symbol(v.value);
				if (s.token != Tokens.Covert("ERR"))
				{
					return s.value;
				}
				else
				{
					error("VAR not found");
				}
				return 0;
			}
			else if (lookahead.token == Tokens.Covert("NUM"))
			{
				return exp();
			}
			else
			{
				error("VAR or NUM expected");
				return -1;
			}
		}

		public virtual void main(string[] args, int argc, char argv)
		{
			if (argc > 1)
			{
				input = lex.read_all_lines(argv[1]);

			}
			else
			{
				Console.WriteLine("Error on read file");

			}
			lookahead = lex.next_Token();
			prg();
			sym.print_symbol_table();
		}
	}

}