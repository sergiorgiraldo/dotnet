/*
 * Created by SharpDevelop.
 * SERGIO RODRIGUES GIRALDO
 * Date: 19/9/2009
 * Time: 22:36
 * 
 */
using System;
using SpecificationAttributeClass;

namespace Specification
{
	public class Usuario
	{
		public Usuario()
		{
		}
		[SpecificationFilter("Nome do Usuário", true)]
		public string Nome{get;set;}
		
		[SpecificationFilter("Cidade do Usuário", true)]
		public string Cidade{get;set;}
		
		[SpecificationFilter("Anos de Estudo", true)]
		public int AnosEscolares{get;set;}

		[SpecificationFilter("Data de Admissão do Usuário", false)]
		public DateTime DataAdmissao{get;set;}
		
		public string Sexo{get;set;}
	}
}
