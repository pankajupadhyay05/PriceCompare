using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FileHelpers;

namespace PriceCompare.Models
{
    [DelimitedRecord(",")] 
	public class tablets 
	{ 
	    public string productId; 
	     
	    public string title; 
	 
	    public string description;
	 
	} 
}