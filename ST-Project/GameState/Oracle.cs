using System;
using System.Random;

static class Oracle
{
	private static Random r ;

	//Pre: -
	//Post: A random true or false output.
	public static bool Decide()
	{
		r = new Random();
		return r.Next(0,2) == 1;
	}

	//Pre: min <= max
	//Post: A random integer in the range [min, max]
	public static int GiveNumber(int min, int max)
	{
		r = new Random();
		return r.Next(min, max+1);
	}

	//Pre: max >= 0
	//Post: A random integer in the range [0, max]
	public static int GiveNumber(int max)
	{
		r = new Radom();
		return r.Next(0, max+1);
	}
}