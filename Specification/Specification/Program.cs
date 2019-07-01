/*
 * Created by SharpDevelop.
 * SERGIO RODRIGUES GIRALDO
 * Date: 19/9/2009
 * Time: 22:28
 * 
 */
using System;
using System.Reflection;
using SpecificationAttributeClass;

namespace Specification
{
	class Program
	{
		public static void Main(string[] args)
		{
			PropertyInfo[] ps = typeof(Usuario).GetProperties(BindingFlags.Public|BindingFlags.Instance);
			
			foreach (PropertyInfo p in ps)
			{
				object[ ] atributes = p.GetCustomAttributes(typeof( SpecificationFilterAttribute ), false ) ;
	
				foreach( Attribute attr in atributes )
	         	{
	           		SpecificationFilterAttribute specificationFilter = (SpecificationFilterAttribute)attr ;
	           		if (specificationFilter.PodeSerUsadoComoFiltro)
	           			Console.WriteLine( "Propriedade:\t" + p.Name + "\tFiltro:\t" + specificationFilter.NomeFiltro ) ;
	         	}			
			}
			Console.ReadKey(true);
		}
	}
}