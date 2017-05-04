using System;
using System.Collections.Generic;


namespace compilador
{

	
	public class Symbols
	{
		internal Token symbol = new Token();
		internal List<Token> symbols = new ArrayList();

		public virtual int add_symbol(Token symbol)
		{

			for (int i = 0; i < symbols.Count; i++)
			{
				Token s = symbols[i];
				if (s.lexem.CompareTo(symbol.lexem) == 0)
				{
					symbols[i] = symbol;
					return i;
				}
			}
			symbols.Add(symbol);
			return symbols.Count - 1;

		}
		public virtual Token get_symbol(int id)
		{
			if (id < symbols.Count)
			{
				return symbols[id];
			}
			Token s = new Token();
			s.token = Tokens.Covert("ERR");
			return s;
		}

		public virtual bool update_symbol(int index, int value)
		{
			if (index > symbols.Count)
			{
				return false;
			}
			symbols[index].value = value;
			return true;
		}

		internal virtual void print_symbol_table()
		{
			for (int i = 0; i < symbols.Count; i++)
			{
				Token s = symbols[i];
				Console.WriteLine("#" + i + " : " + s.token + " : " + s.value + " : " + s.lexem);
			}
		}
	}

}