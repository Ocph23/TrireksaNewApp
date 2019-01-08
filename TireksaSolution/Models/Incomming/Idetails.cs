using System;
 
 namespace Incomming 
{ 

     public interface Idetails  
   {
         int Id {  get; set;} 

         string PenjualanId {  get; set;} 

         string STT {  get; set;} 

         string Pcs {  get; set;} 

         string TypeOfWeight {  get; set;} 

         string Bobot {  get; set;} 

         int ShiperId {  get; set;} 

         int ReciverId {  get; set;} 

         int ManifestId {  get; set;} 

         double Width {  get; set;} 

         double Height {  get; set;} 

         double Longl {  get; set;} 

     }
}


