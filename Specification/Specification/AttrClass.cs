using System ;
   namespace SpecificationAttributeClass
   {
     [AttributeUsage( AttributeTargets.Property, AllowMultiple = false)]
     public class SpecificationFilterAttribute : System.Attribute
     {
       private bool _bPodeSerUsadoComoFiltro ;
	   private string _sNomeFiltro ;
       public SpecificationFilterAttribute(
         string sNomeFiltro, bool bPodeSerUsadoComoFiltro)
       { 
       	_bPodeSerUsadoComoFiltro = bPodeSerUsadoComoFiltro;
       	_sNomeFiltro = sNomeFiltro;
       }
       	public bool PodeSerUsadoComoFiltro{ get { return _bPodeSerUsadoComoFiltro ; } }
		public string NomeFiltro{ get { return _sNomeFiltro ; } }
     }
   }