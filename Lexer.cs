using System;

namespace compilador
{

	public class Lexer
	{
		internal int line = 1;
		internal char peek = ' ';
		internal string input;
		internal int input_pos = -1;
		internal int char_pos = -1;

		public virtual char next_char()
		{
			input_pos++;
			char_pos++;
			char c = '\0';
			if (input_pos < input.Length)
			{
				c = input[input_pos];
			}
			return c;
		}

		internal virtual Token next_Token()
		{
			Token t = new Token();
			Symbols symbol = new Symbols();
			t.token = -1;
			peek = next_char();

			do
			{
				if (peek == ' ' || peek == '\t')
				{
					continue;
				}
				else if (peek == '\n')
				{
					line++;
					char_pos = -1;

				}
				else
				{
					break;
				}
			}while (peek == next_char());

			if (peek == '$')
			{
				string @var = "$";
				peek = next_char();
				while (isLetter(peek))
				{
					@var += peek;
					peek = next_char();
				}
				input_pos--;
				t.token = Tokens.Covert("VAR");
				Token s = new Token();
				s.token = Tokens.Covert("VAR");
				s.value = 0;
				s.lexem = @var;
				t.value = symbol.add_symbol(s);
				return t;
			}
			else if (isDigit(peek))
			{
				int x = 0;
				do
				{
					x = 10 * x + System.identityHashCode(peek);
					peek = next_char();
				}while (isDigit(peek));
				input_pos--;
				t.token = Tokens.Covert("NUM");
				t.value = x;
				return t;
			}
			else if (peek == 'p')
			{
				string print = "print";
				for (int i = 0; i < print.Length; i++)
				{
					if (print[i] == peek)
					{
						peek = next_char();
					}
					else
					{
						t.token = Tokens.Covert("ERR");
						return t;
					}
				}
				t.token = Tokens.Covert("PRINT");
				return t;

			}
			else if (peek == ';')
			{
				t.token = Tokens.Covert("EOL");
				t.value = 0;
			}
			else if (peek == '+')
			{
				t.token = Tokens.Covert("PLUS");
				t.value = 0;
			}
			else if (peek == '=')
			{
				t.token = Tokens.Covert("EQUALS");
				t.value = 0;
			}
			return t;

		}

		public virtual void print_token(Token t)
		{
			if (t.token == Tokens.Covert("ERR"))
			{
				Console.WriteLine("<ERR>");
			}
			else if (t.token == Tokens.Covert("PLUS"))
			{
				Console.WriteLine("<PLUS>");
			}
			else if (t.token == Tokens.Covert("EQUALS"))
			{
				Console.WriteLine("<EQUALS>");
			}
			else if (t.token == Tokens.Covert("NUM"))
			{
				Console.WriteLine("<NUM," + t.value + ">");
			}
			else if (t.token == Tokens.Covert("VAR"))
			{
				Console.WriteLine("<VAR," + t.value + ">");
			}
			else if (t.token == Tokens.Covert("PRINT"))
			{
				Console.WriteLine("<PRINT>");
			}
		}


		internal virtual string read_all_lines(string filename)
		{
			string all_lines = "";
			try
			{
				System.IO.StreamReader file = new System.IO.StreamReader(filename);
				System.IO.StreamReader readFile = new System.IO.StreamReader(file);
				string linha = null;

				while (!string.ReferenceEquals((linha = readFile.ReadLine()), null))
				{
					all_lines += linha;
				}
				readFile.Close();
				return all_lines;
			}
			catch (FileNotFoundException)
			{
				Console.WriteLine("Unable to read file " + filename);
			}
			catch (IOException)
			{
				Console.WriteLine("Error reading file " + filename);
			}


			return "Error";
		}


	}

}