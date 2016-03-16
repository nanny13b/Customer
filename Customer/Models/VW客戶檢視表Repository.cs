using System;
using System.Linq;
using System.Collections.Generic;
	
namespace Customer.Models
{   
	public  class VW客戶檢視表Repository : EFRepository<VW客戶檢視表>, IVW客戶檢視表Repository
	{

	}

	public  interface IVW客戶檢視表Repository : IRepository<VW客戶檢視表>
	{

	}
}